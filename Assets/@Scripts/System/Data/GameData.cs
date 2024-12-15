using System;
using UnityEngine.Serialization;
using static Define;

[Serializable]
public class GameData
{
    public bool isFirstUser = false;
    
    public int gold = 10000000;
    public int mine = 0;
    public int greenGem = 100;
    public int redGem = 100;

    public int level = 1;
    public int exp = 0;
    
    public int[] miningToolLevel = new int[MININGTOOL_COUNT];

    public int[] itemAmount = new int[3];

    public int minerCount = 1;
    
    public int currentMiningToolID = 0;
    
    public bool BGMOn = true;
    public bool effectSoundOn = true;
}

[Serializable]
public class MinerStatsData
{
    public int Damage;
    public int Speed;
}