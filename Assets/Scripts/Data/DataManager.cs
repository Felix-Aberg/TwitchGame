using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    //this script is used to handle logic for reading/writing data

    public StreamerData streamerSessionData;
    public Dictionary<string, PlayerData> playerSessionDataArray;

    public StreamerData streamerTotalData;
    public Dictionary<string, PlayerData> playerTotalDataArray;

    private void Start()
    {
        LoadStreamerData();
        FileWriter.Init();

        playerSessionDataArray = new Dictionary<string, PlayerData>();
        playerTotalDataArray = new Dictionary<string, PlayerData>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            streamerSessionData.shortestGame += 1;
        }
    }

    public void LoadStreamerData()
    {
        streamerSessionData = FileWriter.LoadStreamerData(false);
        streamerTotalData = FileWriter.LoadStreamerData(true);

        if(streamerSessionData == null)
        {
            streamerSessionData = new StreamerData();
            streamerTotalData = new StreamerData();
            Debug.LogWarning("No streamer data loaded! Created a fresh set");
        }
    }


    /*
    static IEnumerator LoadPlayer(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Handle data
            //if entry doesnt exist, add:

            //TODO: turn playerID from int to string
            {
                FileWriter.LoadPlayerData(, false);
                FileWriter.LoadPlayerData(, true);
            } 
        }
    }
    */

    public void LoadPlayerData(string userID)
    {
        //If userID is not already loaded
        if (!playerSessionDataArray.ContainsKey(userID))
        {
            playerSessionDataArray.Add(userID, FileWriter.LoadPlayerData(userID, false));
            playerTotalDataArray.Add(userID, FileWriter.LoadPlayerData(userID, true));
        }

        if (playerSessionDataArray[userID] == null)
        {
            playerSessionDataArray[userID] = new PlayerData();
            playerTotalDataArray[userID] = new PlayerData();
            SetDefaultPlayerData(userID);
            Debug.LogWarning("No player data loaded for " + userID + "! Created a fresh set");
        }
    }

    public void SetDefaultPlayerData(string userID)
    {
        playerSessionDataArray[userID].username = userID;
        playerTotalDataArray[userID].username = userID;
    }

    public void SaveAll()
    {
        //Save streamer data
        FileWriter.SaveStreamerData(streamerSessionData, false);
        FileWriter.SaveStreamerData(streamerTotalData, true);

        Debug.Log(playerSessionDataArray);

        //Save player data
        foreach (KeyValuePair<string, PlayerData> keyValuePair in playerSessionDataArray)
        {
            FileWriter.SavePlayerData(keyValuePair.Value, false);
            FileWriter.SavePlayerData(playerTotalDataArray[keyValuePair.Key], true);
        }
    }
}
