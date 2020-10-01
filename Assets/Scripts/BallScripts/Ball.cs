using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject gameController;
    BallRPM ballRPM;
    BallCollision ballCollision;
    public float minimumRPM;

    public GameObject deathParticles;

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
        if(gameController == null)
        {
            Debug.LogWarning("Ball didn't find GameController when instantiating. This is 100% unintended for normal gameplay.");
            gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
        }

        gameController.GetComponent<KillFeed>().PostKill(name, ballCollision.lastHitBy);

        //TODO: explode

        Instantiate(deathParticles, transform.position, transform.rotation);
        //deathParticles.Play();
        Destroy(gameObject);
    }
}
