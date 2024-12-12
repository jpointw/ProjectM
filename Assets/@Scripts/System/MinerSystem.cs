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
    public Action<int, float> OnStatusChanged;

    private void Start()
    {
        OnEquipmentChanged += HandleEquipmentChanged;
        OnStatusChanged += HandleStatusChanged;
    }

    public void Init()
    {
        HandleEquipmentChanged((MiningToolType)GameSystems.Data.SaveData.currentMiningToolID);
        InstantiateMiners(GameSystems.Data.SaveData.minerCount);
        HandleStatusChanged(0, 0);
    }

    public void HandleEquipmentChanged(MiningToolType miningToolType)
    {
        foreach (MinerController minerController in miners)
        {
            minerController.ChangeMiningTool(miningToolType);
        }
    }
    
    public void HandleStatusChanged(int damange, float speed)
    {
        foreach (MinerController minerController in miners)
        {
            minerController.ChangeMinerStatus(damange, speed);
        }
    }

    public void InstantiateMiners(int count)
    {
        for (int i = 0; i < count; i++)
        {
            MinerController miner = Instantiate(minerControllerObj);
            miners.Add(miner);
        }
    }

    private void OnDestroy()
    {
        OnEquipmentChanged -= OnEquipmentChanged;
        OnStatusChanged -= OnStatusChanged;
    }
}
