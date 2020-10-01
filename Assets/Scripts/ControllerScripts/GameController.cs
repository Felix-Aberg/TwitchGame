using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    BallManager ballManager;
    Canvas startPhaseCanvas;

    [Header("Boolean states")]
    public bool gameStarted;
    public bool gameRunning;
    public bool gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = gameObject.GetComponent<BallManager>();
        startPhaseCanvas = GameObject.Find("StartPhaseCanvas").GetComponent<Canvas>();
        Time.timeScale = 0f;

        gameStarted = false;
        gameRunning = false;
        gameEnded = false;
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
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
