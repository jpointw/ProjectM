using System;
using static Define;

[Serializable]
public class GameData
{
    public int Coin;
    public int Mine = 0;
    public int Gem;
    
    public int[] MiningToolLevel = new int[MININGTOOL_COUNT];
    public int[] MiningToolExp = new int[MININGTOOL_COUNT];

    public int MinerCount = 0;
    
    public int CurrentMiningToolID;
    
    public bool BGMOn = true;
    public bool EffectSoundOn = true;
}