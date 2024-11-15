using Pathfinding;
using UnityEngine;

public class MinerController : MonoBehaviour
{
    private MineBase _targetMine;

    private FollowerEntity _followerEntity;

    public bool IsMining { get; } = false;

    void Start()
    {
        _followerEntity = GetComponent<FollowerEntity>();
    }
    void Update()
    {
        if (_followerEntity.reachedEndOfPath && _targetMine != null) {
            StartMining();
        }
    }

    public void Init()
    {
        
    }

    private void StartMining()
    {
        _targetMine = MineSystem.Instance.GetClosestMine(transform.position);
        SetTargetMine((Mine)_targetMine);
    }
    
    public void SetTargetMine(Mine mine) {
        _targetMine = mine;
        _followerEntity.destination = mine.transform.position;
        _followerEntity.SearchPath();
    }
}
