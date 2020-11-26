﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class KillFeed : MonoBehaviour
{
    int lines;
    public int maxLines;
    public Text killFeed;
    public string killArrow;
    public string selfDestruct;

    private TempScoreDisplay tempScoreDisplay;
    private PlayerCount playerCount;

    bool started = false;

    private void Start()
    {
        tempScoreDisplay = FindObjectOfType<TempScoreDisplay>();
        playerCount = FindObjectOfType<PlayerCount>();

        if (killFeed == null)
        {
            killFeed = GameObject.Find("KillFeed").GetComponent<Text>();
            if (killFeed == null)
            {
                Debug.LogError("Error! Couldn't find the KillFeed text in a canvas!");
            }
            else
            {
                Debug.LogWarning("Warning! KillFeed wasn't assigned before running! Please set it in the gamecontroller killfeed script");
            }
        }

        killFeed.transform.parent.GetComponent<Image>().enabled = false;
    }

    public void PostKill(string killed, string killer)
    {
        if (!started)
        {
            killFeed.transform.parent.GetComponent<Image>().enabled = true;
        }
        lines++;

        bool defaultKill = true;

        if (killed == tempScoreDisplay.bountyName)
        {
            tempScoreDisplay.AddScore(killer, ScoreEvent.BOUNTYKILL);
            killFeed.text = killFeed.text + "\n" + "-v- BOUNTY KILL -v-";
            defaultKill = false;
        }

        if (playerCount.alivePlayers == playerCount.totalPlayers)
        {
            tempScoreDisplay.AddScore(killer, ScoreEvent.FIRSTBLOOD);
            killFeed.text = killFeed.text + "\n" + "-v- FIRST BLOOD -v-";
            defaultKill = false;
        }

        if (defaultKill)
        {
            tempScoreDisplay.AddScore(killer, ScoreEvent.KILL);
        }

        //chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);
        if (killed == "")
        {
            killed = "nobody??";
        }

        if (killFeed.text != "")
        {
            killFeed.text += "\n";
        }

        if (killer == "")
        {
            killFeed.text = killFeed.text + killed + " " + selfDestruct;
            return;
        }

        killFeed.text = killFeed.text + killer + " " + killArrow + " " + killed;

        if (playerCount.alivePlayers == 1)
        {
            return;
        }

        while (lines > maxLines)
        {
            DeleteLine();
        }
    }

    void DeleteLine()
    {
        lines--;

        killFeed.text = killFeed.text.Remove(0, killFeed.text.Split('\n')[0].Length + 1);
    }
}
