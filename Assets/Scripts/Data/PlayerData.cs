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

    public short dataVersion;   //Added, functional, seemingly bug free!
    public int userID;          //To be added
    public string username;     //Added, functional, seemingly bug free!

    public uint deaths;         //Added, functional, seemingly bug free!
    public uint kills;          //Added, functional, seemingly bug free!
    public uint botKills;       //Added, functional, seemingly bug free!
    public uint bountyKills;            //not to be added this time
    public uint firstBloods;    //Added, functional, seemingly bug free!

    public uint gamesPlayed;    
    public uint points;         
    public uint wins;           //Added, functional, seemingly bug free!
    public uint secondPlaces;   //Added, functional, seemingly bug free!
    public uint topFives;       //Added, functional, seemingly bug free!
    public uint totalLifeSpan;  
}
