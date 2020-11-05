using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    BallManager ballManager;
    DataManager dataManager;

    Transform parentCanvas;
    Canvas startPhaseCanvas;
    Canvas nameplateCanvas;

    [Header("Boolean states")]
    public bool gameStarted;
    public bool gameRunning;
    public bool gameEnded;
    bool nameplatesEnabled;

    // Supporting data stuff
    private string[] topTwo;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = gameObject.GetComponent<BallManager>();
        dataManager = gameObject.GetComponent<DataManager>();
        parentCanvas = GameObject.FindWithTag("ParentCanvas").transform;
        startPhaseCanvas = parentCanvas.Find("StartPhaseCanvas").GetComponent<Canvas>();
        nameplateCanvas = parentCanvas.Find("NameplateCanvas").GetComponent<Canvas>();

        GetComponent<Canvas>();
        Time.timeScale = 0f;
        gameStarted = false;
        gameRunning = false;
        gameEnded = false;

        nameplatesEnabled = (PlayerPrefs.GetInt("nameplatesEnabled", 1) == 1) ? true : false;

        topTwo = new string[2];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("SUBMIT") && !gameStarted)
        {
            gameStarted = true;
            startPhaseCanvas.enabled = false;
            Time.timeScale = 1.0f;
        }

        CheckControlInputs();
    }

    void CheckControlInputs()
    {
        if (Input.GetButtonDown("CANCEL"))
        {
            BeforeSceneExit();
            if (SceneManager.GetSceneByName("LevelSelectScene") == null)
            {
                Debug.LogError("ERROR! Couldn't find LevelSelectScene!");
                SceneManager.LoadScene(0);
            }

            SceneManager.LoadScene("LevelSelectScene");

        }

        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            BeforeSceneExit();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
        {
            //Toggle boolean
            nameplatesEnabled = nameplatesEnabled ? false : true;

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
        if (ballManager.parent.childCount == 5)
        {
            //Top 5!
            foreach (KeyValuePair<string, GameObject> entry in ballManager.ballDictionary)
            {
                // do something with entry.Value or entry.Key
                dataManager.playerSessionDataArray[entry.Key].topFives += 1;
                dataManager.playerTotalDataArray[entry.Key].topFives += 1;
            }
        }

        if (ballManager.parent.childCount == 2)
        {
            //Top 2!
            int i = 0;
            foreach (KeyValuePair<string, GameObject> entry in ballManager.ballDictionary)
            {
                topTwo[i] = entry.Key;
                i++;
            }
        }

        if (ballManager.parent.childCount == 1)
        {
            gameEnded = true;

            //Player won!
            string lastPlayerName = ballManager.parent.GetChild(0).name;
            if (ballManager.ballDictionary.ContainsKey(lastPlayerName))
            {
                dataManager.playerSessionDataArray[lastPlayerName].wins += 1;
                dataManager.playerTotalDataArray[lastPlayerName].wins += 1;
            }

            if (topTwo[0] != null)
            {
                foreach (string s in topTwo)
                {
                    if (s != ballManager.ballDictionary.Keys.ToArray()[0])
                    {
                        Debug.Log(s);
                        Debug.Log(dataManager.playerSessionDataArray.ContainsKey(s));
                        //If the ball has loaded data, which only players joining through twitch.tv do
                        if (dataManager.playerSessionDataArray.ContainsKey(s))
                        {
                            dataManager.playerSessionDataArray[s].secondPlaces += 1;
                            dataManager.playerTotalDataArray[s].secondPlaces += 1;
                        }
                    }
                }
            }
            

        }
    }
}
