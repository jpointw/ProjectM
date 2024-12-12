using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class GameSystems : SingletonMonoBase<GameSystems>
{
    /// <summary>
    /// CommonSystems
    /// </summary>
    DataSystem _dataSystem = new DataSystem();
#if UsingAddressable
    ResourceSystem _resourceSystem = new ResourceSystem();
#endif
    SceneSystem _sceneSystem = new SceneSystem();
    
    /// <summary>
    /// InGameSystems
    /// </summary>
    MineSystem  _mineSystem  = null;
    MinerSystem _minerSystem = null;
    EnemySystem _enemySystem = null;
    
    public GameModelDatas gameModelDatas = null;

    public static DataSystem Data => Instance?._dataSystem;
    
    #if usingAddressable
    public static ResourceSystem Resource => Instance?._resourceSystem;
    #endif
    
    public static SceneSystem Scene => Instance?._sceneSystem;
    
    public static MineSystem  MineSystem   =>  Instance?._mineSystem ;
    public static MinerSystem MinerSystem =>  Instance?._minerSystem;
    public static EnemySystem EnemySystem =>  Instance?._enemySystem;
    
    public void Init()
    {
        _dataSystem.Init();
    }

    public bool SetIngameSystems()
    {
        _mineSystem = FindFirstObjectByType<MineSystem>();
        _mineSystem.Init();
        _minerSystem = FindFirstObjectByType<MinerSystem>();
        _minerSystem.Init();
        _enemySystem = FindFirstObjectByType<EnemySystem>();
        _enemySystem.Init();
        return true;
    }
}
