using System;
using UnityEngine;
using static Define;

[CreateAssetMenu(fileName = "GameModelDatas", menuName = "Scriptable Objects/GameModelDatas")]
public class GameModelDatas : ScriptableObject
{
    public LevelUpData[] levelUpDatas;
    public CharacterInfoData[] characterInfoDatas;
    public ItemInfoData[] itemInfoDatas;
    public MiningToolUpgradeData[] MiningToolUpgradeDatas;
    
    public Animation[] miningAnimations;
}

[Serializable]
public struct CharacterInfoData
{
    public int CharacterId;
    public int Damage;
    public int Speed;
}

[Serializable]
public struct LevelUpData
{
    public int level;
    public int exp;
}

[Serializable]
public struct MiningToolUpgradeData
{
    public int ItemLevel;
    public int ItemCost;
}

[Serializable]
public struct MiningToolInfoData
{
    public int itemId;
    public MiningToolType miningToolType;
    public int stopDistance;
    public GameObject miningToolPrefab;
    public Sprite miningToolSprite;
}

[Serializable]
public struct ItemInfoData
{
    public int itemId;
    public Sprite icon;
}