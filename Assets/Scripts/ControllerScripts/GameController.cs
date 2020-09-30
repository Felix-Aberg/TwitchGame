using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    BallManager ballManager;
    Canvas startPhaseCanvas;
    public bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = gameObject.GetComponent<BallManager>();
        startPhaseCanvas = GameObject.Find("StartPhaseCanvas").GetComponent<Canvas>();
        Time.timeScale = 0f;
        gameStarted = false;
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
    }
}
