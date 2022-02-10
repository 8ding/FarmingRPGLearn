
public interface ISaveable
{
    string ISaveableUniqueId
    {
        get; set;
    }

    GameObjectSave GameObjectSave
    {
        get; set;
    }
    void ISaveableRegister();

    void ISaveableDeregister();
    
    void ISaveableStoreScene(string sceneName);

    void ISaveableRestoreScene(string sceneName);
}
