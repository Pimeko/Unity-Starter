using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataController : MonoBehaviour
{
    // [SerializeField]
    // List<BasicGameEvent> saveOn;
    // [SerializeField]
    // List<IRegisterableScriptableObject> variablesToSaveOnChange;
    // [SerializeField]
    // List<BasicGameEvent> loadOn;
    // [SerializeField]
    // bool loadOnAwake;

    // bool JSON_ONLY = false;

    // string dataPath;
	// IPlayerData[] playerDataVariables;

    // void Awake()
    // {
    //     dataPath = Path.Combine(Application.persistentDataPath, "data");
	// 	playerDataVariables = GetComponents<IPlayerData>();

    //     if (loadOnAwake)
    //         Load();

    //     AddListeners();
    // }

    // void AddListeners()
    // {
    //     BasicGameEventListener saveOnListener = gameObject.AddComponent<BasicGameEventListener>();
    //     foreach (BasicGameEvent gameEvent in saveOn)
    //         saveOnListener.Register(gameEvent);
    //     saveOnListener.AddListenerResponse(Save);

    //     foreach (IRegisterableScriptableObject variableToSaveOnChange in variablesToSaveOnChange)
    //         variableToSaveOnChange.AddOnChangeCallback(Save);
        
    //     BasicGameEventListener loadOnListener = gameObject.AddComponent<BasicGameEventListener>();
    //     foreach (BasicGameEvent gameEvent in loadOn)
    //         loadOnListener.Register(gameEvent);
    //     loadOnListener.AddListenerResponse(Load);
    // }

    // void OnDisable()
    // {
    //     foreach (IRegisterableScriptableObject variableToSaveOnChange in variablesToSaveOnChange)
    //         variableToSaveOnChange.RemoveOnChangeCallback(Save);
    // }

    // void FirstSave()
    // {
    //     FileInfo file = new FileInfo(dataPath);
    //     file.Directory.Create();
    //     if (JSON_ONLY)
    //         File.WriteAllText(dataPath, JsonUtility.ToJson(new PlayerData()));
    //     else
    //         File.WriteAllText(dataPath, Base64Encoder.Encode(JsonUtility.ToJson(new PlayerData())));
    // }

    // // Save the content of each related-scriptable objects in the save file
    // public void Save()
    // {
    //     PlayerData playerData = new PlayerData();
	// 	foreach (IPlayerData playerDataController in playerDataVariables)
	// 		playerData = playerDataController.Save(playerData);
        
    //     FileInfo file = new FileInfo(dataPath);
    //     file.Directory.Create();
    //     if (JSON_ONLY)
    //         File.WriteAllText(dataPath, JsonUtility.ToJson(playerData));
    //     else
    //         File.WriteAllText(dataPath, Base64Encoder.Encode(JsonUtility.ToJson(playerData)));
    // }

    // // Load each data in the related-scriptable objects
    // public void Load()
    // {
    //     if (!File.Exists(dataPath))
    //         FirstSave();

    //     PlayerData playerData;
        
    //     if (JSON_ONLY)
    //         playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(dataPath));
    //     else
    //         playerData = JsonUtility.FromJson<PlayerData>(Base64Encoder.Decode(File.ReadAllText(dataPath)));
	// 	foreach (IPlayerData playerDataController in playerDataVariables)
	// 		playerDataController.Load(playerData);
    // }
}