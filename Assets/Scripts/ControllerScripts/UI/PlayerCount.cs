using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour
{
    public int totalPlayers = 0;
    public int alivePlayers = 0;

    public Text playerAmountText;

    private void Start()
    {
        UpdateText();
    }

    public void AddPlayer()
    {
        totalPlayers++;
        alivePlayers++;
        UpdateText();
    }

    public void RemovePlayer()
    {
        alivePlayers--;
        UpdateText();
    }

    private void UpdateText()
    {
        playerAmountText.text = "Players: " + alivePlayers + "/" + totalPlayers + " alive";
    }
}
