using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using static Define;

public class MinerSystem : MonoBehaviour
{
    public static MinerSystem instance;
    
    public MinerController minerControllerObj;
    
    public List<MinerController> miners = new List<MinerController>();
    
    public Animation[] miningAnimations;
    public Action<int> OnCharacterChanged;
    public Action<MiningToolType> OnEquipmentChanged;
    public Action<int, int> OnStatusChanged;

    private void Start()
    {
        OnEquipmentChanged += HandleEquipmentChanged;
        OnStatusChanged += HandleStatusChanged;
    }

    public void Init()
    {
        HandleEquipmentChanged((MiningToolType)GameSystems.Data.SaveData.currentMiningToolID);
        HandleStatusChanged(0, 0);
    }

    public void HandleEquipmentChanged(MiningToolType miningToolType)
    {
        foreach (MinerController minerController in miners)
        {
            minerController.ChangeMiningTool(miningToolType);
        }
    }
    
    public void HandleStatusChanged(int damange, int speed)
    {
        foreach (MinerController minerController in miners)
        {
            minerController.ChangeMinerStatus(damange, speed);
        }
    }

    private void OnDestroy()
    {
        OnEquipmentChanged -= OnEquipmentChanged;
        OnStatusChanged -= OnStatusChanged;
    }
}
