using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchData
{
    Dictionary<string, BallData> ballDataList;

    Dictionary<int, string> placements; //in reverse order!

    int playerJoins;
    int earlyBotJoins;
    int lateBotJoins;

    string firstBlood;

    float prepTime;
    float matchTime;
    float endTime;
}
