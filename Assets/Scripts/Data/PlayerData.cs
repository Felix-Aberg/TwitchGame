using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //This is a datatype to save as a file
    /*
     * user data:
     * id
     * name
     * data version

     * both session and total statistics:
     * kills
     * bot kills
     * deaths (only display as k/d ratio)
     * bounty kills         
     * first bloods
     * points
     * 1st places
     * 2nd places
     * top 5s
     * games played
     * favorite color(???) implement later
     * total lifespan (show average)
     */

    public short dataVersion;
    public int userID;
    public string username;

    public uint deaths;
    public uint kills;
    public uint botKills;
    public uint bountyKills;
    public uint firstBloods;

    public uint gamesPlayed;
    public uint points;
    public uint wins;
    public uint secondPlaces;
    public uint topFives;
    public uint totalLifeSpan;
}
