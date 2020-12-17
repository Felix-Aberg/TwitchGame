using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(TwitchIRCReader))]
public class ChatInputHandler : MonoBehaviour
{
    private GameController gameController;
    private BallManager ballManager;
    private DataManager dataManager;
    private KillFeed killFeed;

    /// <summary>
    /// Takes a message and calls the function accordingly
    /// </summary>
    /// <param name="username">The name of the user, IN LOWERCASE</param>
    /// <param name="message">Keep the exclamation mark from the command!</param>

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        ballManager = FindObjectOfType<BallManager>();
        dataManager = FindObjectOfType<DataManager>();
        killFeed = FindObjectOfType<KillFeed>();

    }

    public void HandleInputs(string username, string message)
    {
        // Check if it's even a command
        if (!message.StartsWith("!"))
        {
            return;
        }

        message = message.Substring(1); // Remove the exclamation mark
        message = message.ToLower();    // Make the message lowercase for easier handling

        if (!gameController.gameStarted)
        {
            StartInputs(username, message);
        }
        else if (gameController.gameStarted)
        {
            CommandInputs(username, message);
        }

    }

    void StartInputs (string username, string message)
    {
        // Return if it doesn't start with "!play" or "!town"
        if (!message.StartsWith("play") && !message.StartsWith("town"))
        {
            return;
        }

        // Load save data
        if (!ballManager.ballDictionary.ContainsKey(username))
        {
            Debug.Log("Loaded player data for: " + username);
            dataManager.LoadPlayerData(username);
        }

        // Spawn ball

        if (message.StartsWith("play ") || message.StartsWith("town "))
        {
            // Remove the first word
            string secondWord = message.Split(' ').Skip(1).FirstOrDefault().ToUpper();

            // Remove all subsequent words
            if (secondWord.Contains(' '))
            {
                secondWord = secondWord.Remove(secondWord.IndexOf(' '));
            }


            if(secondWord == "POGCHAMP")
            {
                Debug.Log("POG");
                GameObject ball = ballManager.AddBall(username);
                ballManager.EquipImageMaterial("Materials/BallIcons/BallMaterialPogChamp", ball);
                PostPlayerJoin(username);
            }
            else if (Enum.TryParse(secondWord, out BallMaterial _))
            {
                Debug.Log("Player successfully specified a ballmaterial");
                ballManager.AddBall(username, (BallMaterial)Enum.Parse(typeof(BallMaterial), secondWord));
                PostPlayerJoin(username);
            }
            else
            {
                Debug.Log("Player failed to specify a ballmaterial");
                ballManager.AddBall(username);
                PostPlayerJoin(username);
            }
        }
        else if (message == "play" || message == "town")
        {
            Debug.Log("Player didn't specify a ballmaterial");
            ballManager.AddBall(username);
            PostPlayerJoin(username);
        }
    }

    void PostPlayerJoin(string username)
    {
        killFeed.PostText(username + " joined the game");
    }

    void CommandInputs (string username, string message)
    {
        //Crit
        if (message.StartsWith("crit"))
        {
            GameObject ball = ballManager.ballDictionary[username].transform.GetChild(0).gameObject;
            //BallCommand cmd = ball.GetComponent<BallCommand>();


            Ball ballScript = ball.GetComponent<Ball>();
            if (!ballScript.abilityActive && ballScript.abilityCharges > 0)
            {
                Debug.Log("Didn't have a BallCommand component!");
                //cmd = ball.AddComponent(typeof(BallCmdCrit)) as BallCommand;
                BallCommand cmd = ball.AddComponent<BallCmdCrit>();
                cmd.ActivateCommand();
                killFeed.PostText(username + " activated their crit ability!");
            }
            else
            {
                Debug.Log("Player did have a BallCommand component!");
            }
        }


        //Ghost
        if (message.StartsWith("ghost"))
        {
            GameObject ball = ballManager.ballDictionary[username].transform.GetChild(0).gameObject;
            //BallCommand cmd = ball.GetComponent<BallCommand>();


            Ball ballScript = ball.GetComponent<Ball>();
            if (!ballScript.abilityActive && ballScript.abilityCharges > 0)
            {
                Debug.Log("Didn't have a BallCommand component!");
                //cmd = ball.AddComponent(typeof(BallCmdCrit)) as BallCommand;
                BallCommand cmd = ball.AddComponent<BallCmdGhost>();
                cmd.ActivateCommand();
                killFeed.PostText(username + " activated their ghost ability!");
            }
            else
            {
                Debug.Log("Player did have a BallCommand component!");
            }
        }

        //Ghost
        if (message.StartsWith("bomb"))
        {
            GameObject ball = ballManager.ballDictionary[username].transform.GetChild(0).gameObject;
            //BallCommand cmd = ball.GetComponent<BallCommand>();


            Ball ballScript = ball.GetComponent<Ball>();
            if (!ballScript.abilityActive && ballScript.abilityCharges > 0)
            {
                Debug.Log("Didn't have a BallCommand component!");
                //cmd = ball.AddComponent(typeof(BallCmdCrit)) as BallCommand;
                BallCommand cmd = ball.AddComponent<BallCmdBomb>();
                cmd.ActivateCommand();
                killFeed.PostText(username + " activated their bomb ability!");
            }
            else
            {
                Debug.Log("Player did have a BallCommand component!");
            }
        }
    }
}
