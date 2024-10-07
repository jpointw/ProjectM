using System;
using Pathfinding;
using UnityEngine;

public class TargetInject : MonoBehaviour
{
    public GameObject target;
    
    public FollowerEntity follower;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            follower.destination = target.transform.position;
            follower.SearchPath();
        }
    }
}
