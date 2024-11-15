using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using static Define;


public class MineSystem : SingletonMonoBase<MineSystem>
{
    public GameObject plane;

    public MineBase goldMinePrefab;
    public MineBase coalMinePrefab;
    public MineBase ironMinePrefab;
    public MineBase diamondMinePrefab;

    public List<MineBase> activeMines;

    private Dictionary<MineType, ObjectPool<MineBase>> minePools;
    
    public const int MaxMineAmount = 20;

    void Start()
    {
        minePools = new Dictionary<MineType, ObjectPool<MineBase>>()
        {
            { MineType.Gold, CreateMinePool(goldMinePrefab, 5, 10) },
            { MineType.Coal, CreateMinePool(coalMinePrefab, 5, 10) },
            { MineType.Iron, CreateMinePool(ironMinePrefab, 5, 10) },
            { MineType.Diamond, CreateMinePool(diamondMinePrefab, 5, 10) }
        };

        for (int i = 0; i < MaxMineAmount; i++)
        {
            RequestNewMine(plane.transform.position, 5);
        }
        
        MonitorMinesAsync().Forget();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RequestNewMine(plane.transform.position, 5);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            activeMines[Random.Range(0, activeMines.Count)].MineConsumed(10f);
        }
    }
    
    private async UniTaskVoid MonitorMinesAsync(float delay = 5f)
    {
        while (true)
        {
            if (activeMines.Count < MaxMineAmount)
            {
                RequestNewMine(plane.transform.position, 5); 
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
        }
    }

    private ObjectPool<MineBase> CreateMinePool(MineBase prefab, int defaultCapacity, int maxSize)
    {
        
        return new ObjectPool<MineBase>(
            createFunc: () => Instantiate(prefab),
            actionOnGet: mine => {
                activeMines.Add(mine);
                mine.gameObject.SetActive(true);
            },
            actionOnRelease: mine => {
                activeMines.Remove(mine);
                mine.ResetMine();
                
                mine.gameObject.SetActive(false);
            },
            actionOnDestroy: mine => Destroy(mine.gameObject),
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public MineBase RequestNewMine(Vector3 planePosition, float minDistanceBetweenMines)
    {
        MineBase newMine = null;
        int randomValue = UnityEngine.Random.Range(0, 100);

        newMine = randomValue switch
        {
            < 40 => minePools[MineType.Gold].Get(),
            < 70 => minePools[MineType.Coal].Get(),
            < 90 => minePools[MineType.Iron].Get(),
            _ => minePools[MineType.Diamond].Get()
        };

        if (newMine != null)
        {
            int maxAttempts = 10;
            bool validPosition = false;

            for (int attempts = 0; attempts < maxAttempts && !validPosition; attempts++)
            {
                Vector3 randomPosition = GenerateRandomPositionOnPlane(planePosition);

                Collider[] hitColliders = Physics.OverlapSphere(randomPosition, minDistanceBetweenMines, 6);

                if (hitColliders.Length == 0)
                {
                    newMine.transform.position = randomPosition;
                    validPosition = true;
                }
            }

            if (!validPosition)
            {
                minePools[newMine.mineType].Release(newMine);
                newMine = null;
            }
        }

        return newMine;
    }

    private Vector3 GenerateRandomPositionOnPlane(Vector3 planePosition)
    {
        Vector3 planeScale = plane.transform.localScale;

        float randomX = UnityEngine.Random.Range(-0.5f * planeScale.x * 10, 0.5f * planeScale.x * 10);
        float randomZ = UnityEngine.Random.Range(-0.5f * planeScale.z * 10, 0.5f * planeScale.z * 10);

        return new Vector3(planePosition.x + randomX, planePosition.y, planePosition.z + randomZ);
    }

    public void ReturnMineToPool(MineBase mine)
    {
        if (minePools.ContainsKey(mine.mineType))
            minePools[mine.mineType].Release(mine);
    }

    public MineBase GetClosestMine(Vector3 minerPosition)
    {
        MineBase closestMine = null;
        float closestDistance = Mathf.Infinity;

        foreach (var mine in activeMines)
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
            closestMine.MineTargeted(true);

        return closestMine;
    }
}