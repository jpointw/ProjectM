using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random; // Unity의 Object Pool 네임스페이스

public class MineSystem : SingletonMonoBase<MineSystem>
{
    public 
    
    public MineBase goldMinePrefab;     
    public MineBase coalMinePrefab;     
    public MineBase ironMinePrefab;     
    public MineBase diamondMinePrefab;  

    private ObjectPool<MineBase> goldMinePool;    
    private ObjectPool<MineBase> coalMinePool;    
    private ObjectPool<MineBase> ironMinePool;    
    private ObjectPool<MineBase> diamondMinePool; 

    public List<MineBase> activeMines; 

    void Start()
    {
        goldMinePool = new ObjectPool<MineBase>(
            createFunc: () => Instantiate(goldMinePrefab),
            actionOnGet: mine => {
                mine.gameObject.SetActive(true);
                activeMines.Add(mine);
            },
            actionOnRelease: mine => {
                mine.ResetMine();
                mine.gameObject.SetActive(false);
                activeMines.Remove(mine);
            },
            actionOnDestroy: mine => Destroy(mine.gameObject),
            defaultCapacity: 3,
            maxSize: 5         
        );

        coalMinePool = new ObjectPool<MineBase>(
            createFunc: () => Instantiate(coalMinePrefab),
            actionOnGet: mine => {
                mine.gameObject.SetActive(true);
                activeMines.Add(mine);
            },
            actionOnRelease: mine => {
                mine.ResetMine();
                mine.gameObject.SetActive(false);
                activeMines.Remove(mine);
            },
            actionOnDestroy: mine => Destroy(mine.gameObject),
            defaultCapacity: 3,
            maxSize: 5
        );

        ironMinePool = new ObjectPool<MineBase>(
            createFunc: () => Instantiate(ironMinePrefab),
            actionOnGet: mine => {
                mine.gameObject.SetActive(true);
                activeMines.Add(mine);
            },
            actionOnRelease: mine => {
                mine.ResetMine();
                mine.gameObject.SetActive(false);
                activeMines.Remove(mine);
            },
            actionOnDestroy: mine => Destroy(mine.gameObject),
            defaultCapacity: 2,
            maxSize: 4
        );

        diamondMinePool = new ObjectPool<MineBase>(
            createFunc: () => Instantiate(diamondMinePrefab),
            actionOnGet: mine => {
                mine.gameObject.SetActive(true);
                activeMines.Add(mine);
            },
            actionOnRelease: mine => {
                mine.ResetMine();
                mine.gameObject.SetActive(false);
                activeMines.Remove(mine);
            },
            actionOnDestroy: mine => Destroy(mine.gameObject),
            defaultCapacity: 1,
            maxSize: 2
        );
    }

    public MineBase RequestNewMine(Vector3 position)
    {
        MineBase newMine = null;

        int randomValue = Random.Range(0, 100);

        newMine = randomValue switch
        {
            < 40 => goldMinePool.Get(),
            < 70 => coalMinePool.Get(),
            < 90 => ironMinePool.Get(),
            _ => diamondMinePool.Get()
        };

        if (newMine != null)
        {
            newMine.transform.position = position;
        }

        return newMine;
    }

    public void ReturnMineToPool(MineBase mine)
    {
        switch (mine.mineType)
        {
            case MineType.GOLD:
                goldMinePool.Release(mine);
                break;
            case MineType.COAL:
                goldMinePool.Release(mine);
                break;
            case MineType.IRON:
                goldMinePool.Release(mine);
                break;
            case MineType.DIAMOND:
                goldMinePool.Release(mine);
                break;
        }
    }

    public MineBase GetClosestMine(Vector3 minerPosition)
    {
        MineBase closestMine = null;
        float closestDistance = Mathf.Infinity;

        foreach (MineBase mine in activeMines)
        {
            if (mine.IsTargeted || mine.IsConsumed) continue;

            float distanceToMine = Vector3.Distance(minerPosition, mine.transform.position);
            if (distanceToMine < closestDistance)
            {
                closestDistance = distanceToMine;
                closestMine = mine;
            }
        }

        if (closestMine != null)
        {
            closestMine.MineTargeted(true);
        }

        return closestMine;
    }
}
