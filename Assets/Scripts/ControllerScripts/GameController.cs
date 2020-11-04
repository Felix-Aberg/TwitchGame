using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    BallManager ballManager;

    Transform parentCanvas;
    Canvas startPhaseCanvas;
    Canvas nameplateCanvas;

    [Header("Boolean states")]
    public bool gameStarted;
    public bool gameRunning;
    public bool gameEnded;
    bool nameplatesEnabled;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = gameObject.GetComponent<BallManager>();
        parentCanvas = GameObject.FindWithTag("ParentCanvas").transform;
        startPhaseCanvas = parentCanvas.Find("StartPhaseCanvas").GetComponent<Canvas>();
        nameplateCanvas = parentCanvas.Find("NameplateCanvas").GetComponent<Canvas>();

        GetComponent<Canvas>();
        Time.timeScale = 0f;
        gameStarted = false;
        gameRunning = false;
        gameEnded = false;

        nameplatesEnabled = (PlayerPrefs.GetInt("nameplatesEnabled", 1) == 1) ? true : false;
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
            if (SceneManager.GetSceneByName("LevelSelectScene") == null)
            {
                Debug.LogError("ERROR! Couldn't find LevelSelectScene!");
                SceneManager.LoadScene(0);
            }

            SceneManager.LoadScene("LevelSelectScene");

        }

        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N))
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

    private void CheckWin()
    {
        if(ballManager.parent.childCount == 1)
        {

        }
    }
}
