
using System;
using UnityEngine;

public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instacne;

    public static T Instance
    {
        get
        {
            return instacne;
        }
    }

    protected  virtual void Awake()
    {
        if(instacne == null)
        {
            instacne = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
