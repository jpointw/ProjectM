using UnityEngine;

public class GameScene : SceneBase
{
    protected override bool Init()
    {
        base.Init();
        return GameSystems.Instance.SetIngameSystems();
    }
}
