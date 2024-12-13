using UnityEngine;

public static class MinerExtensions
{
    public static int GetDamage()
    {
        var modelDatas = GameSystems.ModelDatas;
        var saveData = GameSystems.Data.SaveData;
        int baseDamage = modelDatas.characterInfoDatas[0].Damage;

        int levelBonusDamage = saveData.level * 15;

        int toolBonusDamage = saveData.miningToolLevel[saveData.currentMiningToolID] * 15;

        int totalDamage = baseDamage + levelBonusDamage + toolBonusDamage;
        
        return totalDamage;
    }

    public static float GetSpped()
    {
        var modelDatas = GameSystems.ModelDatas;
        var saveData = GameSystems.Data.SaveData;
        float speed = modelDatas.characterInfoDatas[0].Speed;

        float levelBonusSpeed = saveData.level * 0.1f;

        float toolBonusSpeed = saveData.miningToolLevel[saveData.currentMiningToolID] * 0.1f;
        
        float totalSpeed = speed + levelBonusSpeed + toolBonusSpeed + toolBonusSpeed;
        
        return totalSpeed;
    }
}