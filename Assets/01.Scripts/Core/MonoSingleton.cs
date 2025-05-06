using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;
    public static bool IsDestroyed = false;

    public static T Instance
    {
        get
        {
            if(IsDestroyed)
            {
                _instance = null;
            }
            if(_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<T>();
                if(_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} singleton is not exist");
                }
                else
                {
                    IsDestroyed = false;
                }
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        IsDestroyed = true;
    }
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            ///Destroy(gameObject);
        }
    }
    
    
}