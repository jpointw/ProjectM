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
    public Action OnMinerAdded;

    private void Start()
    {
        OnEquipmentChanged += HandleEquipmentChanged;
        OnStatusChanged += HandleStatusChanged;
        OnMinerAdded += HandleAddMiner;
    }

    public void Init()
    {
        HandleEquipmentChanged((MiningToolType)GameSystems.Data.SaveData.currentMiningToolID);
        InstantiateAllMiners(GameSystems.Data.SaveData.minerCount); 
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
        Debug.LogError("이거 혹시 실행되니?");
    }

    public void InstantiateAllMiners(int count)
    {
        for (int i = 0; i < count; i++)
        {
            MinerController miner = Instantiate(minerControllerObj);
            miners.Add(miner);
            Debug.LogError(MinerExtensions.GetDamage());
            int a = MinerExtensions.GetDamage();
            miner.ChangeMinerStatus(MinerExtensions.GetDamage(), MinerExtensions.GetSpped());
            miner.SetTargetMine();

        }
    }

    public void HandleAddMiner()
    {
        MinerController miner = Instantiate(minerControllerObj);
        miners.Add(miner);
        miner.ChangeMinerStatus(MinerExtensions.GetDamage(), MinerExtensions.GetSpped());
        miner.SetTargetMine();
    }

    private void OnDestroy()
    {
        OnEquipmentChanged -= OnEquipmentChanged;
        OnStatusChanged -= OnStatusChanged;
    }
}
