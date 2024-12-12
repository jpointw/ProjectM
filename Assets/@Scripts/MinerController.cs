using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using static Define;

public class MinerController : MonoBehaviour
{
    public GameObject[] miningTools;

    public MineBase _targetMine;

    private FollowerEntity _followerEntity;
    private MinerAnimation _minerAnimation;

    private Coroutine _miningCoroutine;

    [FormerlySerializedAs("damage")] public int Damage = 0;

    public bool IsMining { get; private set; } = false;
    private bool _hasReachedDestination = false;

    void Start()
    {
        _followerEntity = GetComponent<FollowerEntity>();
        _minerAnimation = GetComponent<MinerAnimation>();
    }

    void Update()
    {
        if (_followerEntity.reachedEndOfPath && !_hasReachedDestination && _targetMine != null)
        {
            _hasReachedDestination = true;
            StartMining();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetTargetMine();
        }
    }

    public void Init()
    {
        _followerEntity ??= GetComponent<FollowerEntity>();
        SetTargetMine();
    }

    private void StartMining()
    {
        if (IsMining || _targetMine == null) return;

        IsMining = true;

        if (_miningCoroutine != null)
        {
            StopCoroutine(_miningCoroutine);
        }

        _miningCoroutine = StartCoroutine(MiningCoroutine());
    }

    private IEnumerator MiningCoroutine()
    {
        while (_targetMine != null && !_targetMine.IsConsumed)
        {
            Debug.Log("Mining...");
            _minerAnimation?.MiningAnimationStart();
            _targetMine.MineConsumed(Damage);
            if (_targetMine.IsConsumed)
            {
                FinishMining();
                yield break;
            }

            yield return new WaitForSeconds(1.16f);
        }
    }

    private void FinishMining()
    {
        IsMining = false;

        _minerAnimation?.DefaultAnimationStart();
        
        if (_miningCoroutine != null)
        {
            StopCoroutine(_miningCoroutine);
            _miningCoroutine = null;
        }
        if (_targetMine != null)
        {
            _targetMine.MineTargeted(false);
            _targetMine = null;
        }
        SetTargetMine();
    }

    public void SetTargetMine()
    {
        IsMining = false;
        _hasReachedDestination = false;

        _targetMine = MineSystem.Instance.GetClosestMine(transform.position);

        if (_targetMine == null)
        {
            Debug.LogWarning("근처에 채굴 가능한 광산이 없습니다!");
            return;
        }

        _targetMine.MineTargeted(true);
        _followerEntity.destination = _targetMine.transform.position;
        _followerEntity.SearchPath();
    }

    public void ChangeMiningTool(MiningToolType miningToolType)
    {
        switch (miningToolType)
        {
            case MiningToolType.Hand:
                foreach (var miningTool in miningTools)
                {
                    miningTool.gameObject.SetActive(false);
                }
                break;
            case MiningToolType.Pickaxe:
                miningTools[0].gameObject.SetActive(true);
                miningTools[1].gameObject.SetActive(false);
                break;
            case MiningToolType.Bomb:
                miningTools[0].gameObject.SetActive(false);
                miningTools[1].gameObject.SetActive(true);
                break;
            case MiningToolType.Turret:
                break;}
    }

    public void ChangeMinerStatus(int damage, float? speed)
    {
        Damage = damage;
        if (speed != null) _followerEntity.maxSpeed = (float)speed;
    }

    private void OnDestroy()
    {
        if (_miningCoroutine != null)
        {
            StopCoroutine(_miningCoroutine);
        }
    }
}
