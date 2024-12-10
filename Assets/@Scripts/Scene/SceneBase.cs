using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SceneBase : MonoBehaviour
{
    public SceneType SceneType = SceneType.Unknown;
    protected bool _init = false;

    public void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        GameSystems.Instance.Init();

        // GameObject go = GameObject.Find("EventSystem");
        // if (go == null)
        // {
        //     GameSystems.Resource.Instantiate("EventSystem", null, (go) =>
        //     {
        //         go.name = "@EventSystem";
        //     });
        // }

        return true;
    }

    public virtual void Clear()  
    {

    }
}