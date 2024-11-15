using UnityEngine;

public abstract class SingletonMonoBase<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();

                if (_instance == null)
                {
                    GameObject container = new GameObject($"Singleton {typeof(T)}");
                    _instance = container.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }
        else
        {
            if (_instance != this)
            {
                if (GetComponents<Component>().Length <= 2)
                    Destroy(gameObject);
                else
                    Destroy(this);
            }
        }

        AwakeAfterSingletonInit();
    }

    protected virtual void AwakeAfterSingletonInit() { }
}