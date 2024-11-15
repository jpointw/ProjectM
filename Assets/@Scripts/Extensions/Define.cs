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
        Pickaxe,
        Drill,
        Laser,
    }

    public enum SceneType
    {
        Unknown,
        GameScene,
        TitleScene,
    }

    public const int MININGTOOL_COUNT = 0;
}
