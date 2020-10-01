using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject gameController;
    BallRPM ballRPM;
    BallCollision ballCollision;
    public float minimumRPM;

    // Start is called before the first frame update
    void Start()
    {
        ballRPM = GetComponent<BallRPM>();
        ballCollision = GetComponent<BallCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -50f)
        {
            SelfDestruct();
        }

        if (ballRPM.RPM < minimumRPM)
        {
            SelfDestruct();
        }


    }

    public void SelfDestruct()
    {
        //Call killfeed
        gameController.GetComponent<KillFeed>().PostKill(name, ballCollision.lastHitBy);

        //TODO: explode

        Destroy(gameObject);
    }
}
