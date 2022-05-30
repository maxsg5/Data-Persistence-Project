using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance; // static instance of this class
    public string playerName; // playerName of the player
    public int score; // score of the player

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Load(); // Load the data from the file
    }


    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.score = score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.playerName;
            score = data.score;
        }
    }
}
