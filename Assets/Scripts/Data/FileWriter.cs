using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileWriter : MonoBehaviour
{
    //This script is to read/write data
    private static readonly string savePathRoot = Application.persistentDataPath + "/Saves";
    private static readonly string savePathPlayerRoot = savePathRoot + "/Players";
    private static readonly string savePathStreamerRoot = savePathRoot + "/Streamer";
    private static readonly string savePathTotal = "/Total";
    private static readonly string fileType = ".txt";
    private static readonly int sessionID = (int)Time.time;

    // ##### PLAYER FUNCTIONS ###

    public static void Init()
    {

    }

    public static string GetFilePath(string userID, bool isTotal)
    {
        string returnPath = savePathPlayerRoot;

        if (isTotal)
        {
            returnPath += "/" + savePathTotal + "/" + userID.ToString() + fileType;
        }
        else
        {
            returnPath += "/" + sessionID.ToString() + "/" + userID.ToString() + fileType;
        }

        return returnPath;
    }

    public static string GetDirectoryPath(bool isTotal)
    {
        string returnPath = savePathPlayerRoot;

        if (isTotal)
        {
            returnPath += "/" + savePathTotal + "/";
        }
        else
        {
            returnPath += "/" + sessionID.ToString() + "/";
        }

        return returnPath;
    }

    //WARNING: This save OVERWRITES ANY PREVIOUSLY STORED DATA
    public static void SavePlayerData(PlayerData playerData, bool isTotal)
    {
        Debug.Log("PlayerData: " + playerData);

        //TODO: TURN INTO USERID INSTEAD OF USERNAME
        string savePath = GetFilePath(playerData.username, isTotal);
        string saveDirectory = GetDirectoryPath(isTotal);

        if (!Directory.Exists(saveDirectory))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(saveDirectory);
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, playerData);
        }
        Debug.Log("saved");
    }

    /// <summary> Get file path for streamer's save file </summary>
    public static PlayerData LoadPlayerData(string userID, bool isTotal)
    {
        string savePath = GetFilePath(userID, isTotal);

        PlayerData playerData;

        if (File.Exists(savePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                playerData = (PlayerData)binaryFormatter.Deserialize(fileStream);
            }

            return playerData;
        }
        return null;
    }

    // ##### STREAMER FUNCTIONS ###

    /// <summary> Get file path for streamer's save file </summary>
    public static string GetFilePath(bool isTotal)
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
    public static void SaveStreamerData(StreamerData streamerData, bool isTotal)
    {
        string savePath = GetFilePath(isTotal);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using (FileStream fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, streamerData);
        }
    }

    public static StreamerData LoadStreamerData(bool isTotal)
    {
        string savePath = GetFilePath(isTotal);

        StreamerData streamerData;
        if (File.Exists(savePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                streamerData = (StreamerData)binaryFormatter.Deserialize(fileStream);
            }

            return streamerData;
        }

        return null;
    }
}
