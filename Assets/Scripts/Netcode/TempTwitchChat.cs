using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class TempTwitchChat : MonoBehaviour
{
    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    //DO NOT MAKE THIS VARIABLE PUBLIC
    private string[] userdata; //Get the password from https://twitchapps.com/tmi

    public Text chatBox;
    private GameController gameController;
    private BallManager ballManager;
    private DataManager dataManager;

    //Move these into GetUserdata() later

    void Start()
    {
        gameController = GetComponent<GameController>();
        ballManager = GetComponent<BallManager>();
        dataManager = GetComponent<DataManager>();

        userdata = GetUserdata();
        Connect();
    }

    void Update()
    {
        if (!twitchClient.Connected)
        {
            Connect();
        }

        ReadChat();
    }

    //Store your password in /Assets/Resources/userdata.txt
    //If the file does not exist, create it. It is not included in the git repository.
    private string[] GetUserdata()
    {
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/userdata.txt");

        if (lines == null)
        {
            Debug.LogError("Error! userdata.txt file could not be found! Please see the userdata.txt file in /Assets/Resources/");
        }
        
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Remove(0, 12);
        }
        
        return lines;
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + userdata[2]);
        writer.WriteLine("NICK " + userdata[1]);
        writer.WriteLine("USER " + userdata[1] + " 8 * :" + userdata[1]);
        writer.WriteLine("JOIN #" + userdata[0]);
        writer.Flush();
    }

    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine(); //Read in the current message

            if (message.Contains("PRIVMSG"))
            {
                //Get the users name by splitting it from the string
                Debug.Log(message);
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //Capitalise first letter of name
                char[] name = chatName.ToCharArray();
                name[0] = char.ToUpper(name[0]);
                chatName = new string(name);

                //Get the users message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                //print(String.Format("{0}: {1}", chatName, message));
                chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);

                //Run the instructions to control the game!
                GameInputs(message, chatName);
            }
        }
    }

    private void GameInputs(string ChatInputs, string ChatName)
    {


        ChatInputs = ChatInputs.ToLower();
        if (!gameController.gameStarted && ChatInputs.StartsWith("!play"))
        {
            //Load save data
            if (!ballManager.ballDictionary.ContainsKey(ChatName))
            {
                Debug.Log("Loaded player data for: " + ChatName);
                dataManager.LoadPlayerData(ChatName);
            }


            //!play | Spawn ball
            if (ChatInputs.StartsWith("!play "))
            {
                Debug.Log("Stage 1");
                string secondWord = ChatInputs.Split(' ').Skip(1).FirstOrDefault().ToUpper();

                if (secondWord.Contains(' '))
                {
                    secondWord = secondWord.Remove(secondWord.IndexOf(' '));
                }

                Debug.Log("Stage 3");
                if (Enum.TryParse(secondWord, out BallMaterial _))
                {
                    Debug.Log("Player successfully specified a ballmaterial");
                    ballManager.AddBall(ChatName, (BallMaterial)Enum.Parse(typeof(BallMaterial), secondWord));
                }
                else
                {
                    Debug.Log("Player failed to specify a ballmaterial");
                    ballManager.AddBall(ChatName);
                }
            }
            else if (ChatInputs == "!play")
            {
                Debug.Log("Player didn't specify a ballmaterial");
                ballManager.AddBall(ChatName);
            }
        }
        else if (gameController.gameStarted)
        {
            //!
            if (ChatInputs.StartsWith("!crit"))
            {
                GameObject ball = ballManager.ballDictionary[ChatName].transform.GetChild(0).gameObject;
                BallCommand cmd = ball.GetComponent<BallCommand>();
                if (cmd == null)
                {
                    Debug.Log("Didn't have a BallCommand component!");
                    cmd = ball.AddComponent(typeof(BallCmdCrit)) as BallCommand;
                    cmd.ActivateCommand();
                }
                else
                {
                    Debug.Log("Player did have a BallCommand component!");
                }
            }
        }
    }
}