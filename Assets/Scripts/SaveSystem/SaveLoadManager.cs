using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveLoadManager : SingletonMonoBehavior<SaveLoadManager>
{
    public List<ISaveable> iSaveableObjectList;

    protected override void Awake()
    {
        base.Awake();
        iSaveableObjectList = new List<ISaveable>();
    }


    

    public void StoreCurrentSceneData()
    {
        foreach (var VARIABLE in iSaveableObjectList)
        {
            VARIABLE.ISaveableStoreScene(SceneManager.GetActiveScene().name);
        }
    }

    public void RestoreCurrentSceneData()
    {
        foreach (var VARIABLE in iSaveableObjectList)
        {
            VARIABLE.ISaveableRestoreScene(SceneManager.GetActiveScene().name);
        }
    }
}
