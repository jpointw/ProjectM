using System;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public EnemyController[] enemies;

    public Action OnKillItemUsed;


    private void Start()
    {
        OnKillItemUsed += KillAllEnemies;
    }

    public void KillAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.KillMyself();
        }
    }

    public void Init()
    {
        throw new NotImplementedException();
    }
}
