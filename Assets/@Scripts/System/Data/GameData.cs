using System;
using UnityEngine.Serialization;
using static Define;

[Serializable]
public class GameData
{
    public int gold;
    public int mine = 0;
    public int greenGem;
    public int redGem;

    public int level;
    public int exp;
    
    public int[] miningToolLevel = new int[MININGTOOL_COUNT];

    public int[] itemAmount;

    public int minerCount = 0;
    
    public int currentMiningToolID;
    
    public bool BGMOn = true;
    public bool effectSoundOn = true;
}

[Serializable]
public class MinerStatsData
{
    public int Damage;
    public int Speed;
}