using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : Component
{
    static T _instatnce;
    public static T Instatnce { get { return _instatnce; } }

    /// <summary>
    /// Awakeで実行したい処理を書く
    /// </summary>
    protected abstract void DoAwake();

    protected void Awake()
    {
        if(_instatnce == null)
        {
            _instatnce = this as T;
            DontDestroyOnLoad(gameObject);
            DoAwake();
        }
        else
            Destroy(gameObject);
    }
}
