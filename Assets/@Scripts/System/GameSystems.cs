using UnityEngine;

public class GameSystems : SingletonMonoBase<GameSystems>
{


    /// <summary>
    /// CommonSystems
    /// </summary>
    DataSystem _dataSystem = new DataSystem();
    ResourceSystem _resourceSystem = new ResourceSystem();
    SceneSystem _sceneSystem = new SceneSystem();
    
    /// <summary>
    /// InGameSystems
    /// </summary>
    MineSystem _mineSystem = null;
    MinerSystem _minerSystem = null;

    public static DataSystem Data => Instance?._dataSystem;
    public static ResourceSystem Resource => Instance?._resourceSystem;
    
    public static SceneSystem Scene => Instance?._sceneSystem;
    
    public void Init()
    {
        
    }
}
