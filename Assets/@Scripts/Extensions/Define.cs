using UnityEngine;

public class Define
{
    public enum MineType
    {
        Gold,
        Coal,
        Iron,
        Diamond
    }
    
    public enum MiningToolType
    {
        Hand,
        Pickaxe,
        Bomb,
        Turret,
    }

    public enum SceneType
    {
        Unknown,
        GameScene,
        TitleScene,
    }

    public const int MININGTOOL_COUNT = (int)MiningToolType.Turret + 1;
    public const int MINETYPE_COUNT = (int)MineType.Diamond + 1;
    public const int MAX_MINER_COUNT = 10;
}
