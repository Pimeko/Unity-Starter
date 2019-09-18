using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerDataController : SerializedMonoBehaviour
{
    [SerializeField]
    List<BasicGameEvent> saveOn;
    [SerializeField]
    List<RegisterableScriptableObject> variablesToSaveOnChange;
    [SerializeField]
    List<BasicGameEvent> loadOn;
    [SerializeField]
    BetterEvent onLoad;
    [SerializeField]
    bool loadOnAwake;
    [SerializeField]
    bool JSON_ONLY = false;

    string dataPath;
	IPlayerData[] playerDataVariables;

    void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "data");
		playerDataVariables = GetComponents<IPlayerData>();

        if (loadOnAwake)
            Load();

        AddListeners();
    }

    void AddListeners()
    {
        if (saveOn.Count > 0)
        {
            BasicGameEventListener saveOnListener = gameObject.AddComponent<BasicGameEventListener>();
            foreach (BasicGameEvent gameEvent in saveOn)
                saveOnListener.AddGameEvent(gameEvent);
            saveOnListener.AddCallback(Save);
        }
        
        if (loadOn.Count > 0)
        {
            BasicGameEventListener loadOnListener = gameObject.AddComponent<BasicGameEventListener>();
            foreach (BasicGameEvent gameEvent in loadOn)
                loadOnListener.AddGameEvent(gameEvent);
            loadOnListener.AddCallback(Load);
        }

        foreach (RegisterableScriptableObject variableToSaveOnChange in variablesToSaveOnChange)
            variableToSaveOnChange.AddOnChangeCallback(Save);
    }

    void OnDisable()
    {
        foreach (IRegisterableScriptableObject variableToSaveOnChange in variablesToSaveOnChange)
            variableToSaveOnChange.RemoveOnChangeCallback(Save);
    }

    void FirstSave()
    {
        FileInfo file = new FileInfo(dataPath);
        file.Directory.Create();
        PlayerData playerData = new PlayerData();
		foreach (IPlayerData playerDataController in playerDataVariables)
			playerDataController.Init(ref playerData);
        string jsonEncoded = JsonUtility.ToJson(playerData);
        File.WriteAllText(dataPath, JSON_ONLY ? jsonEncoded : Base64Encoder.Encode(jsonEncoded));
    }

    // Save the content of each related-scriptable objects in the save file
    public void Save()
    {
        PlayerData playerData = new PlayerData();
		foreach (IPlayerData playerDataController in playerDataVariables)
			playerDataController.Save(ref playerData);
        string jsonEncoded = JsonUtility.ToJson(playerData);
        
        FileInfo file = new FileInfo(dataPath);
        file.Directory.Create();
        File.WriteAllText(dataPath, JSON_ONLY ? jsonEncoded : Base64Encoder.Encode(jsonEncoded));
    }

    // Load each data in the related-scriptable objects
    public void Load()
    {
        if (!File.Exists(dataPath))
            FirstSave();

        PlayerData playerData;
        string asJSON = File.ReadAllText(dataPath);
        playerData = JsonUtility.FromJson<PlayerData>(JSON_ONLY ? asJSON : Base64Encoder.Decode(asJSON));
		
        foreach (IPlayerData playerDataController in playerDataVariables)
			playerDataController.Load(playerData);
        onLoad.Invoke();
    }
}