using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int savedScore;
}

[Serializable]
public class PositionData
{
    public Vector3 position;
}

[Serializable]
public class JsonSaveData
{
    public PositionData playerPosition;
    public List<PositionData> enemyPositions;
}
public class ScoreSaveSystem : ISaveable
{
    private string saveFilePath;

    public ScoreSaveSystem()
    {
        string userName = System.Environment.UserName;
        Debug.Log("User: " + userName);
        saveFilePath = Path.Combine(Application.persistentDataPath, "C:/Users/" + userName + "/Documents/saveData.dat");
        
    }

    public void Save(int score)
    {
        SaveData data = new SaveData { savedScore = score };
        using (FileStream file = File.Create(saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, data);
        }
    }

    public int Load()
    {
        if (File.Exists(saveFilePath))
        {
            using (FileStream file = File.Open(saveFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SaveData data = (SaveData)formatter.Deserialize(file);
                return data.savedScore;
            }
        }
        return 0;
    }
}

public class JsonSaveSystem
{
    private string saveFilePath;

    public JsonSaveSystem()
    {
        string userName = System.Environment.UserName;
        Debug.Log("User: " + userName);
        saveFilePath = Path.Combine(Application.persistentDataPath, "C:/Users/" + userName + "/Documents/positions.json");
    }

    public void Save(Vector3 playerPosition, List<Vector3> enemyPositions)
    {
        JsonSaveData data = new JsonSaveData
        {
            playerPosition = new PositionData { position = playerPosition },
            enemyPositions = new List<PositionData>()
        };

        foreach (var enemyPos in enemyPositions)
        {
            data.enemyPositions.Add(new PositionData { position = enemyPos });
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    public JsonSaveData Load()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<JsonSaveData>(json);
        }
        return null;
    }
}
