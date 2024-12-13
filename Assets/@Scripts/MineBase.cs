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

        switch (mineType)
        {
            case MineType.Gold:
                mineAmount = 500;
                break;
            case MineType.Coal:
                mineAmount = 500;
                break;
            case MineType.Iron:
                mineAmount = 1500;
                break;
            case MineType.Diamond:
                mineAmount = 3000;
                break;
        }
    }
    
    
}