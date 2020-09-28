using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    BallManager ballManager;
    public bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = gameObject.GetComponent<BallManager>();
        Time.timeScale = 0f;
        gameStarted = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("SUBMIT") && !gameStarted)
        {
            gameStarted = true;
            Time.timeScale = 1.0f;
        }
    }
}
