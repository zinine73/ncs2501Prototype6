using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Color TeamColor;
    private void Awake()
    {
        TeamColor = Color.red;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = this.TeamColor;

        string json = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(Application.persistentDataPath
                + "/savefile.json", json);
        }
        catch (Exception ex)
        {
            Debug.Log($"Failed to save Json : {ex}");
        }
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath
            + "/savefile.json";
        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                this.TeamColor = data.TeamColor;
            }
            else
            {
                Debug.Log("File not exist");
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Faile to load Json : {ex}");
        }
    }
}
