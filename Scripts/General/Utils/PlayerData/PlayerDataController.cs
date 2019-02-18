using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    [SerializeField]
    List<GameEvent> saveOn;
    [SerializeField]
    List<GameEvent> loadOn;
    [SerializeField]
    bool loadOnStart;

    string dataPath;
	IPlayerData[] playerDataVariables;

    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "data");
		playerDataVariables = GetComponents<IPlayerData>();

        AddListeners();

        if (loadOnStart)
            Load();
    }

    void AddListeners()
    {
        GameEventListener saveOnListener = gameObject.AddComponent<GameEventListener>();
        foreach (GameEvent gameEvent in saveOn)
            saveOnListener.Register(gameEvent);
        saveOnListener.AddListenerResponse(Save);

        GameEventListener loadOnListener = gameObject.AddComponent<GameEventListener>();
        foreach (GameEvent gameEvent in loadOn)
            loadOnListener.Register(gameEvent);
        loadOnListener.AddListenerResponse(Load);
    }

    void FirstSave()
    {
        FileInfo file = new FileInfo(dataPath);
        file.Directory.Create();
        File.WriteAllText(dataPath, Base64Encoder.Encode(JsonUtility.ToJson(new PlayerData())));
    }

    // Save the content of each related-scriptable objects in the save file
    public void Save()
    {
        PlayerData playerData = new PlayerData();
		foreach (IPlayerData playerDataController in playerDataVariables)
			playerData = playerDataController.Save(playerData);
        
        FileInfo file = new FileInfo(dataPath);
        file.Directory.Create();
        File.WriteAllText(dataPath, Base64Encoder.Encode(JsonUtility.ToJson(playerData)));
    }

    // Load each data in the related-scriptable objects
    public void Load()
    {
        if (!File.Exists(dataPath))
            FirstSave();

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(Base64Encoder.Decode(File.ReadAllText(dataPath)));
		foreach (IPlayerData playerDataController in playerDataVariables)
			playerDataController.Load(playerData);
    }
}