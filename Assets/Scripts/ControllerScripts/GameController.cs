using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    BallManager ballManager;
    DataManager dataManager;
    PlayerCount playerCount;
    TempScoreDisplay tempScoreDisplay;

    Transform parentCanvas;
    Canvas startPhaseCanvas;
    Canvas nameplateCanvas;

    [Header("Boolean states")]
    public bool gameStarted;
    public bool gameRunning;
    public bool gameEnded;
    public bool commandsEnabled;
    bool nameplatesEnabled;

    int playersLastFrame = 0;
    int counter = 0;
    // Supporting data stuff
    public string[] topTwo;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = gameObject.GetComponent<BallManager>();
        dataManager = gameObject.GetComponent<DataManager>();
        playerCount = gameObject.GetComponent<PlayerCount>();
        tempScoreDisplay = FindObjectOfType<TempScoreDisplay>();
        parentCanvas = GameObject.FindWithTag("ParentCanvas").transform;
        startPhaseCanvas = parentCanvas.Find("StartPhaseCanvas").GetComponent<Canvas>();
        nameplateCanvas = parentCanvas.Find("NameplateCanvas").GetComponent<Canvas>();

        GetComponent<Canvas>();
        Time.timeScale = 0f;
        gameStarted = false;
        gameRunning = false;
        gameEnded = false;

        nameplatesEnabled = (PlayerPrefs.GetInt("nameplatesEnabled", 1) == 1) ? true : false;
        commandsEnabled = (PlayerPrefs.GetInt("commandsEnabled", 1) == 1) ? true : false;

        topTwo = new string[2];
    }

    // Update is called once per frame
    private void Update()
    {
        CheckControlInputs();
    }

    void CheckControlInputs()
    {
        if (Input.GetButton("SUBMIT") && !gameStarted)
        {
            gameStarted = true;
            startPhaseCanvas.enabled = false;
            Time.timeScale = 1.0f;
        }

        if (Input.GetButtonDown("CANCEL"))
        {
            BeforeSceneExit();
            if (SceneManager.GetSceneByName("LevelSelect") == null)
            {
                Debug.LogError("ERROR! Couldn't find LevelSelect!");
                SceneManager.LoadScene(0);
            }

            SceneManager.LoadScene("LevelSelect");

        }

        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            BeforeSceneExit();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.LeftControl))
        {
            //Toggle boolean
            commandsEnabled = !commandsEnabled;

            if (commandsEnabled)
            {
                PlayerPrefs.SetInt("nameplatesEnabled", 1);
            }
            else
            {
                PlayerPrefs.SetInt("nameplatesEnabled", 0);
            }
        }


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        {
            //Toggle boolean
            nameplatesEnabled = !nameplatesEnabled;

            if (nameplatesEnabled)
            {
                PlayerPrefs.SetInt("nameplatesEnabled", 1);
                nameplateCanvas.enabled = true;
            }
            else
            {
                PlayerPrefs.SetInt("nameplatesEnabled", 0);
                nameplateCanvas.enabled = false;
            }
        }

        if (gameStarted && !gameEnded)
        {
            CheckWin();
        }
    }

    /// <summary>
    /// Call this before leaving the scene! Wether restarting or gonig to a different level
    /// </summary>
    private void BeforeSceneExit()
    {
        GetComponent<DataManager>().SaveAll();
    }

    private void CheckWin()
    {
        if (playersLastFrame != 5 && playerCount.alivePlayers == 5)
        {
            Debug.Log("Top 5!");
            //Top 5!
            foreach (Transform child in ballManager.parent)
            {
                if (!child.GetChild(0).GetComponent<Ball>().isBot)
                {
                    //Statistics: Top 5 placement
                    dataManager.playerSessionDataArray[child.name].topFives += 1;
                    dataManager.playerTotalDataArray[child.name].topFives += 1;
                }
            }
        }

        if (playersLastFrame != 2 && playerCount.alivePlayers == 2)
        {
            Debug.Log("Top 2!");
            //Top 2!
            int i = 0;

            foreach (Transform child in ballManager.parent)
            {
                topTwo[i] = child.name;
                i++;
            }
        }

        if (playersLastFrame != 1 && playerCount.alivePlayers == 1)
        {
            gameEnded = true;
            Debug.Log("Game ended!");

            //Player won!
            string lastPlayerName = ballManager.parent.GetChild(0).name;
            if (dataManager.playerTotalDataArray.ContainsKey(lastPlayerName))
            {
                //Statistics: 1st place win
                dataManager.playerSessionDataArray[lastPlayerName].wins += 1;
                dataManager.playerTotalDataArray[lastPlayerName].wins += 1;

                Debug.Log("Adding score to winner! Name: " + lastPlayerName);
                tempScoreDisplay.AddScore(lastPlayerName, ScoreEvent.PLACE1);
            }

            if (topTwo[0] != null)
            {
                for (int i = 0; i < topTwo.Length; i++) 
                {
                    if (topTwo[i] != ballManager.parent.GetChild(0).name)
                    {
                        //2nd place identified

                        //If the ball has loaded data, which only players joining through twitch.tv do
                        if (dataManager.playerSessionDataArray.ContainsKey(topTwo[i]))
                        {
                            //Statistics: 2nd place
                            dataManager.playerSessionDataArray[topTwo[i]].secondPlaces += 1;
                            dataManager.playerTotalDataArray[topTwo[i]].secondPlaces += 1;

                            Debug.Log("Adding score to first loser! Name: " + topTwo[i]);
                            tempScoreDisplay.AddScore(topTwo[i], ScoreEvent.PLACE2);
                        }
                    }
                }
            }
        }

        playersLastFrame = playerCount.alivePlayers;
    }
}
