using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeed : MonoBehaviour
{
    int lines;
    public int maxLines;
    public Text killFeed;
    public string killArrow;
    public string selfDestruct;

    public void PostKill(string killed, string killer)
    {
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

        if (killer == "")
        {
            killFeed.text = killFeed.text + killed + " " + selfDestruct + "\n";
            return;
        }

        killFeed.text = killFeed.text + killer + " " + killArrow + " " + killed + "\n";

    }

    void DeleteLine()
    {
        lines--;

        killFeed.text = killFeed.text.Remove(0, killFeed.text.Split('\n')[0].Length + 1);
    }
}
