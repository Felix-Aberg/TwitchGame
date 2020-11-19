using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    //this script is used to handle logic for reading/writing data

    //This is where you change this variable, nowhere else
    private short currentDataVersion = 1;

    FileWriter fileWriter;

    public StreamerData streamerSessionData;
    public Dictionary<string, PlayerData> playerSessionDataArray;

    public StreamerData streamerTotalData;
    public Dictionary<string, PlayerData> playerTotalDataArray;

    private void Start()
    {
        fileWriter = gameObject.AddComponent<FileWriter>();
        fileWriter.Init();
        LoadStreamerData();

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
        streamerSessionData = fileWriter.LoadStreamerData(false);
        streamerTotalData = fileWriter.LoadStreamerData(true);

        if(streamerSessionData == null)
        {
            streamerSessionData = DefaultStreamerData();
            Debug.LogWarning("No streamer session data loaded! Created a fresh set");
        }

        if (streamerTotalData == null)
        {
            streamerTotalData = DefaultStreamerData();
            Debug.LogWarning("No streamer total data loaded! Created a fresh set");
        }
    }

    public void LoadPlayerData(string userID)
    {
        //TOTAL PLAYER DATA
        //If userID is not already loaded
        if (!playerSessionDataArray.ContainsKey(userID))
        {
            playerTotalDataArray.Add(userID, fileWriter.LoadPlayerData(userID, true));
        }

        if (playerTotalDataArray[userID] == null)
        {
            playerTotalDataArray[userID] = DefaultPlayerData(userID);

            //playerSessionDataArray.Add(userID, new PlayerData());
            //playerTotalDataArray.Add(userID, new PlayerData());
            Debug.LogWarning("No player data loaded for " + userID + "! Created a fresh set");
        }

        //SESSION PLAYER DATA
        //If userID is not already loaded
        if (!playerSessionDataArray.ContainsKey(userID))
        {
            playerSessionDataArray.Add(userID, fileWriter.LoadPlayerData(userID, false));
        }

        if (playerSessionDataArray[userID] == null || playerSessionDataArray[userID].dataVersion == 0)
        {
            playerSessionDataArray[userID] = DefaultPlayerData(userID);

            //playerSessionDataArray.Add(userID, new PlayerData());
            //playerTotalDataArray.Add(userID, new PlayerData());
            Debug.LogWarning("No player data loaded for " + userID + "! Created a fresh set");
        }
    }

    /// <summary>
    /// Set the default values of playerdata
    /// </summary>
    /// <param name="userID">Username of the player. Will eventually be userID</param>
    public PlayerData DefaultPlayerData(string userID)
    {
        PlayerData playerData = new PlayerData();
        playerData.dataVersion              = currentDataVersion;
        //playerSessionDataArray[userID].userID = userID;                               //To be implemented
        playerData.username                 = userID;               //Gets set elsewhere already

        playerData.deaths                   = 0;
        playerData.kills                    = 0;
        playerData.botKills                 = 0;
        playerData.bountyKills              = 0;
        playerData.firstBloods              = 0;

        playerData.gamesPlayed              = 0;
        playerData.points                   = 0;
        playerData.wins                     = 0;
        playerData.secondPlaces             = 0;
        playerData.topFives                 = 0;
        playerData.totalLifeSpan            = 0;

        return playerData;
    }

    public StreamerData DefaultStreamerData()
    {
        StreamerData streamerData = new StreamerData();
        streamerData.dataVersion = currentDataVersion;

        streamerData.gamesPlayed            = 0;
        streamerData.totalJoins             = 0;
        streamerData.uniqueJoins            = 0;

        streamerData.longestGame            = 0;
        streamerData.shortestGame           = 0;
        streamerData.totalGameLength        = 0;
        return streamerData;
    }

    public void SaveAll()
    {
        //Save streamer data
        fileWriter.SaveStreamerData(streamerSessionData, false);
        fileWriter.SaveStreamerData(streamerTotalData, true);

        //Save player data
        foreach (KeyValuePair<string, PlayerData> keyValuePair in playerSessionDataArray)
        {
            fileWriter.SavePlayerData(playerSessionDataArray[keyValuePair.Key], false);
            fileWriter.SavePlayerData(playerTotalDataArray[keyValuePair.Key], true);
        }
    }
}
