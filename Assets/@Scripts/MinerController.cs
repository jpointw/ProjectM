using Pathfinding;
using UnityEngine;

public class MinerController : MonoBehaviour
{
    private MineBase _targetMine;

    private FollowerEntity _followerEntity;
    
    void Start()
    {
        _followerEntity = GetComponent<FollowerEntity>();
        _targetMine = MineSystem.Instance.GetClosestMine(this.transform.position);
        StartMining();
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
        Debug.LogError($"{gameObject.name} is starting mining");
        _targetMine = MineSystem.Instance.GetClosestMine(transform.position);
        SetTargetMine((Mine)_targetMine);
    }
    
    public void SetTargetMine(Mine mine) {
        _targetMine = mine;
        _followerEntity.destination = mine.transform.position;
        _followerEntity.SearchPath();
        Debug.LogError($"{mine.gameObject.name} is moving to the target mine");
    }
}
