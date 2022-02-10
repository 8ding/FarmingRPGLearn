using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GenerateGUID))]
public class SceneItemManager : SingletonMonoBehavior<SceneItemManager>,ISaveable
{
    private Transform parentItem;
    [SerializeField] private GameObject itemPrefab = null;

    private string iSaveableUniqueID;
    public string ISaveableUniqueId
    {
        get
        {
            return iSaveableUniqueID;
        }
        set
        {
            iSaveableUniqueID = value;
        }
    }
    private GameObjectSave gameObjectSave;
    public GameObjectSave GameObjectSave
    {
        get
        {
            return gameObjectSave;
        }
        set
        {
            gameObjectSave = value;
        }
    }

    private void AfterSceneLoad()
    {
        parentItem = GameObject.FindWithTag(Tags.ItemParentTransform).transform;
    }

    protected override void Awake()
    {
        base.Awake();
        ISaveableUniqueId = GetComponent<GenerateGUID>().GUID;
        GameObjectSave = new GameObjectSave();
    }

    private void OnEnable()
    {
        ISaveableRegister();
        EventHandler.AfterSceneLoadEvent += AfterSceneLoad;
    }

    private void OnDisable()
    {
        ISaveableDeregister();
        EventHandler.AfterSceneLoadEvent -= AfterSceneLoad;
    }

    private void DestroySceneItems()
    {
        Item[] itemInScene = GameObject.FindObjectsOfType<Item>();
        for (int i = itemInScene.Length - 1; i > -1; i--)
        {
            Destroy(itemInScene[i].gameObject);
        }
    }

    public void InstantiateSceneItem(int itemCode, Vector3 itemPosition)
    {
        GameObject itemGameObject = Instantiate(itemPrefab, itemPosition, Quaternion.identity, parentItem);
        Item item = itemGameObject.GetComponent<Item>();
        item.Init(itemCode);
    }

    public void ISaveableRegister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Add(this);
    }

    public void ISaveableDeregister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Remove(this);
    }

    public void ISaveableStoreScene(string sceneName)
    {
        Item[] itemInScene = GameObject.FindObjectsOfType<Item>();
        SceneSave sceneSave = new SceneSave();
        sceneSave.listSceneDictionary = new Dictionary<string, List<SceneItem>>();
        List<SceneItem> sceneItems = new List<SceneItem>();
        for (int i = 0; i < itemInScene.Length; i++)
        {
            sceneItems.Add(new SceneItem(itemInScene[i]));
        }
        sceneSave.listSceneDictionary["SceneItemList"] = sceneItems;
        if(GameObjectSave != null)
        {
            GameObjectSave.sceneData.Remove("SceneItemList");
            GameObjectSave.sceneData[sceneName] = sceneSave;
        }
        else
        {
            GameObjectSave = new GameObjectSave(new Dictionary<string, SceneSave>
            {
                {sceneName, sceneSave}
            });
        }
    }

    public void ISaveableRestoreScene(string sceneName)
    {
        
        if(GameObjectSave != null)
        {
            if(GameObjectSave.sceneData.ContainsKey(sceneName))
            {
               
                if(GameObjectSave.sceneData[sceneName].listSceneDictionary.ContainsKey("SceneItemList"))
                {
                    DestroySceneItems();
                    List<SceneItem> sceneItems = GameObjectSave.sceneData[sceneName].listSceneDictionary["SceneItemList"];
                    for (int i = 0; i < sceneItems.Count; i++)
                    {
                        InstantiateSceneItem(sceneItems[i].itemCode, new Vector3(sceneItems[i].position.x, sceneItems[i].position.y, sceneItems[i].position.z));
                    }
                }
            }

        }
    }
}
