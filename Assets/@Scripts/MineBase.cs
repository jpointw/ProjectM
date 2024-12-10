using System;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class MineBase : MonoBehaviour
{
    public MineType mineType;
    
    public MinerController minerController;
    
    protected bool isConsumed = false;
    protected bool isTargeted = false;
    
    
    protected float mineAmount = 100;
    
    protected MineAnimation mineAnimation;

    public bool IsConsumed => isConsumed;
    public bool IsTargeted => isTargeted;

    protected virtual void Start()
    {
        mineAnimation = GetComponent<MineAnimation>();
    }

    public void MineTargeted(bool istargeted)
    {
        isTargeted = istargeted;
    }
    
    public void MineConsumed(float amount)
    {
        mineAnimation.ShakeOnce();
        mineAmount -= amount;
        if (mineAmount <= 0)
        {
            mineAmount = 0;
            isConsumed = true;
            MineSystem.Instance.ReturnMineToPool(this);
        }
    }
    
    public virtual void ResetMine()
    {
        
    }

    public virtual void InitMine()
    {
        isConsumed = false;
        isTargeted = false;
        mineAmount = 100;
    }
    
    
}