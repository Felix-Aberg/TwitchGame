using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileWriter : MonoBehaviour
{
    //This script is to read/write data
    private string savePathRoot;
    private string savePathPlayerRoot;
    private string savePathStreamerRoot;
    private string savePathTotal;
    private string fileType;

    //To be replaced
    //private static readonly string sessionID = "0";

    private string sessionID; //TODO: doesn't work


    // ##### PLAYER FUNCTIONS ###

    public void Init()
    {
        savePathRoot = Application.persistentDataPath + "/Saves";
        savePathPlayerRoot = savePathRoot + "/Players";
        savePathStreamerRoot = savePathRoot + "/Streamer";
        savePathTotal = "/Total";
        fileType = ".txt";

    sessionID = System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")
                                        .Replace("/", "").Replace(":", "");
    }

    public string GetFilePath(string userID, bool isTotal)
    {
        string returnPath = savePathPlayerRoot;

        if (isTotal)
        {
            returnPath += "/" + savePathTotal + "/" + userID + fileType;
        }
        else
        {
            returnPath += "/" + sessionID.ToString() + "/" + userID + fileType;
        }
        return returnPath;
    }

    public string GetDirectoryPath(bool isTotal, bool isUser)
    {
        string returnPath;

        if (isUser)
        {
            returnPath = savePathPlayerRoot;

            if (isTotal)
            {
                returnPath += "/" + savePathTotal + "/";
            }
            else
            {
                returnPath += "/" + sessionID.ToString() + "/";
            }
        }
        else
        {
            returnPath = savePathStreamerRoot + "/";
        }

        return returnPath;
    }

    //WARNING: This save OVERWRITES ANY PREVIOUSLY STORED DATA
    public void SavePlayerData(PlayerData playerData, bool isTotal)
    {
        Debug.Log("PlayerData: " + playerData);

        //TODO: TURN INTO USERID INSTEAD OF USERNAME
        string savePath = GetFilePath(playerData.username, isTotal);
        string saveDirectory = GetDirectoryPath(isTotal, true);

        if (!Directory.Exists(saveDirectory))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(saveDirectory);
            Debug.Log("Directory doesn't exist! Creating it! " + saveDirectory);
        }
        /*
        //Serialize Method
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, playerData);


        }
        */

        //JSON Method
        string output = JsonUtility.ToJson(playerData, true);
        using (StreamWriter newTask = new StreamWriter(savePath, false))
        {
            newTask.WriteLine(output);
        }
    }

    /// <summary> Get file path for streamer's save file </summary>
    public PlayerData LoadPlayerData(string userID, bool isTotal)
    {
        string savePath = GetFilePath(userID, isTotal);

        PlayerData playerData;

        if (File.Exists(savePath))
        {
            /*
            //Serialize Method
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            }
            */

            string input = File.ReadAllText(savePath);
            return playerData = JsonUtility.FromJson<PlayerData>(input);
        }
        return null;
    }

    // ##### STREAMER FUNCTIONS ###

    /// <summary> Get file path for streamer's save file </summary>
    public string GetFilePath(bool isTotal)
    {
        string returnPath = savePathStreamerRoot;

        if (isTotal)
        {
            returnPath += savePathTotal + fileType;

        }
        else
        {
            returnPath += "/" + sessionID.ToString() + fileType;
        }
        return returnPath;
    }

    /// <summary> WARNING: This save OVERWRITES ANY PREVIOUSLY STORED DATA </summary>
    public void SaveStreamerData(StreamerData streamerData, bool isTotal)
    {
        string savePath = GetFilePath(isTotal);
        string saveDirectory = GetDirectoryPath(isTotal, false);
        if (!Directory.Exists(saveDirectory))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(saveDirectory);
        }

        /*
        //Serialize Method
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, streamerData);
        }
        */

        //JSON Method
        string output = JsonUtility.ToJson(streamerData, true);
        using (StreamWriter newTask = new StreamWriter(savePath, false))
        {
            newTask.WriteLine(output);
        }
    }

    public StreamerData LoadStreamerData(bool isTotal)
    {
        string savePath = GetFilePath(isTotal);

        StreamerData streamerData;
        if (File.Exists(savePath))
        {
            /*
            //Serialize Method
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                streamerData = (StreamerData)binaryFormatter.Deserialize(fileStream);
            }
            */

            //JSON Method
            string input = File.ReadAllText(savePath);
            return streamerData = JsonUtility.FromJson<StreamerData>(input);
        }

        return null;
    }
}
