using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Needed for state saving
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManagement : MonoBehaviour
{

    public static DataManagement datamanagement;

    // my variables
    public int tokensHighScore;

    void Awake()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
    }

    public void SaveData()
    {
        BinaryFormatter binform = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "GameInfo.dat");
        gameData data = new gameData();
        data.tokensHighScore = tokensHighScore;
        binform.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "GameInfo.dat"))
        {
            BinaryFormatter binform = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "GameInfo.dat", FileMode.Open);
            gameData data = (gameData)binform.Deserialize(file);
            file.Close();
            tokensHighScore = data.tokensHighScore;
        }
    }

}

[Serializable]
class gameData
{
    public int tokensHighScore;
}
