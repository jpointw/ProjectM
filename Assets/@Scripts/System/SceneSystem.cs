using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem
{
    private Define.SceneType _curSceneType = Define.SceneType.Unknown;

    public Define.SceneType CurrentSceneType
    {
        get
        {
            if (_curSceneType != Define.SceneType.Unknown)
                return _curSceneType;
            return current.SceneType;
        }
        set {  _curSceneType = value; }
    }

    public SceneBase current { get { return GameObject.FindObjectOfType<SceneBase>(); } }

    public void ChangeScene(Define.SceneType type)
    {
        Debug.Log(current);
        current.Clear();

        _curSceneType = type;
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.SceneType type)
    {
        string name = System.Enum.GetName(typeof(Define.SceneType), type);
        char[] letters = name.ToLower().ToCharArray();
        letters[0] = char.ToUpper(letters[0]);
        return new string(letters);
    }
}