using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

public class TempTwitchChat : MonoBehaviour
{
    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    string[] userdata; //Get the password from https://twitchapps.com/tmi

    public Text chatBox;
    private BallManager ballManager;

    void Start()
    {
        ballManager = GetComponent<BallManager>();
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

    //Store your password in /Assets/Resources/Userdata.txt
    //If the file does not exist, create it. It is not included in the git repository.
    private string[] FetchPassword()
    {
        TextAsset textAsset = Resources.Load("userdata.txt") as TextAsset;
        String text = textAsset.ToString();
        String[] lines = text.Split('\n');

        if (textAsset == null)
        {
            Debug.LogError("Error! userdata.txt file could not be found! Please include a Password.txt fine in /Assets/Resources/");
        }
        

        return;
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + userdata[1]);
        writer.WriteLine("NICK " + userdata[2]);
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
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //Get the users message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                //print(String.Format("{0}: {1}", chatName, message));
                chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);

                //Run the instructions to control the game!
                GameInputs(message, chatName);
            }
        }
        else
        {
            Debug.LogWarning("Twitch chat client failed to access!");
        }
    }

    private void GameInputs(string ChatInputs, string ChatName)
    {
        if (ChatInputs.ToLower() == ("!play"))
        {
            ballManager.AddBall(ChatName);
        }
    }
}