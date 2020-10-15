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

    bool started;

    private void Start()
    {
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

        if (lines > maxLines)
        {
            DeleteLine();
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

    }

    void DeleteLine()
    {
        lines--;

        killFeed.text = killFeed.text.Remove(0, killFeed.text.Split('\n')[0].Length + 1);
    }
}
