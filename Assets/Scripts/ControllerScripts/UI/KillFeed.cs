using System;
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
    private DataManager dataManager;

    bool started = false;

    private void Start()
    {
        tempScoreDisplay = FindObjectOfType<TempScoreDisplay>();
        playerCount = FindObjectOfType<PlayerCount>();
        dataManager = FindObjectOfType<DataManager>();

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

    /// <summary>
    /// Kills are posted by the dead player.
    /// </summary>
    public void PostKill(string killed, string killer)
    {

        if (!started)
        {
            killFeed.transform.parent.GetComponent<Image>().enabled = true;
        }

        //If it's a bot
        if (!dataManager.playerSessionDataArray.ContainsKey(killer))
        {
            return; //Give no points
        }

        // Kill is determined to be posted beyond this point, so run InitMessage();
        InitMessage();

        bool defaultKill = true;

        if (killed == tempScoreDisplay.bountyName)
        {
            tempScoreDisplay.AddScore(killer, ScoreEvent.BOUNTYKILL);
            killFeed.text = killFeed.text + "-v- BOUNTY KILL -v-" + "\n";
            defaultKill = false;
        }

        if (playerCount.alivePlayers == playerCount.totalPlayers)
        {
            tempScoreDisplay.AddScore(killer, ScoreEvent.FIRSTBLOOD);
            killFeed.text = killFeed.text + "-v- FIRST BLOOD -v-" + "\n";
            defaultKill = false;
        }

        //Return if player killed itself (with bomb, for example)
        if (killer == killed)
        {
            killFeed.text = killFeed.text + killed + " " + selfDestruct;
            return;
        }

        if (defaultKill && playerCount.alivePlayers > 1)
        {
            tempScoreDisplay.AddScore(killer, ScoreEvent.KILL);
        }

        //chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);
        if (killed == "")
        {
            killed = "nobody??";
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

        ExitMessage();
    }

    public void PostText(string message)
    {
        InitMessage();

        killFeed.text += message;

        ExitMessage();
    }

    void InitMessage()
    {
        if (killFeed.text != "")
        {
            killFeed.text += "\n";
        }
        lines++;
    }

    void ExitMessage()
    {
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
