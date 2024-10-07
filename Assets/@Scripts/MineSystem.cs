using System.Collections.Generic;
using UnityEngine;

public class MineSystem : SingletonMonoBase<MineSystem>
{
    public List<Mine> mines;
    
    public AstarPath Astar;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init()
    {
        
    }
    
    public Mine GetClosestMine(Vector3 minerPosition) {
        Mine closestMine = null;
        var closestDistance = Mathf.Infinity;

        foreach (Mine mine in mines) {
            if (mine.IsTargeted) continue;
            var distanceToMine = Vector3.Distance(minerPosition, mine.transform.position);

            if (distanceToMine < closestDistance) {
                closestDistance = distanceToMine;
                closestMine = mine;
            }
        }

        closestMine.MineTargeted(true);
        return closestMine;
    }
}
