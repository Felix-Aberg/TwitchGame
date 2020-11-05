using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StreamerData
{
    //This is a datatype to save as a file
    /*
     * data version

     * both session and total statistics:
     * games played
     * total joins
     * unique joins //Make work later
     * longest game
     * shortest game
     * total game length (for average)
     */

    public short dataVersion;

    public uint gamesPlayed;
    public uint totalJoins;
    public uint uniqueJoins;

    public float longestGame;
    public float shortestGame;
    public float totalGameLength;
}
