using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isBot;
    public GameObject gameController;
    BallDurability ballDur;
    BallCollision ballCollision;
    NameplateDisplay nameplateDisplay;

    public float minimumRPM;
    public GameObject deathParticles;

    public float abilityCharges = 3; //set to 2 later
    public bool abilityActive;

    // Start is called before the first frame update
    void Start()
    {
        name = transform.parent.name;
        ballDur = GetComponent<BallDurability>();
        ballCollision = GetComponent<BallCollision>();
        nameplateDisplay = GetComponent<NameplateDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
        {
            if (transform.position.y < -50f)
            {
                SelfDestruct();
            }

            if (ballDur.RPM < minimumRPM)
            {
                SelfDestruct();
            }
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

        gameController.GetComponent<KillFeed>().PostKill(transform.parent.name, ballCollision.lastHitByName);
        gameController.GetComponent<PlayerCount>().RemovePlayer();

        if(!isBot)
        {
            //Statistics: Add death to player
            gameController.GetComponent<DataManager>().playerSessionDataArray[name].deaths += 1;
            gameController.GetComponent<DataManager>().playerTotalDataArray[name].deaths += 1;
        }
        
        if (ballCollision.lastHitByGameObject != null)
        {
            //Gain RPM on kill
            ballCollision.lastHitByGameObject.GetComponent<BallDurability>().RPM += ballCollision.ballConfig.DurOnKill;

            if (!ballCollision.lastHitByGameObject.GetComponent<Ball>().isBot)
            {
                if (isBot)
                {
                    //Statistics: Add bot kill to player
                    gameController.GetComponent<DataManager>().playerSessionDataArray[ballCollision.lastHitByName].botKills += 1;
                    gameController.GetComponent<DataManager>().playerTotalDataArray[ballCollision.lastHitByName].botKills += 1;
                }
                else
                {
                    //Statistics: Add kill to player
                    gameController.GetComponent<DataManager>().playerSessionDataArray[ballCollision.lastHitByName].kills += 1;
                    gameController.GetComponent<DataManager>().playerTotalDataArray[ballCollision.lastHitByName].kills += 1;
                }

                PlayerCount playerCount = gameController.GetComponent<PlayerCount>();
                if (playerCount.totalPlayers == playerCount.alivePlayers + 1)
                {
                    //Statistics: Add bounty kill to player
                    gameController.GetComponent<DataManager>().playerSessionDataArray[ballCollision.lastHitByName].firstBloods += 1;
                    gameController.GetComponent<DataManager>().playerTotalDataArray[ballCollision.lastHitByName].firstBloods += 1;
                }
            }
        }
        
        Instantiate(deathParticles, transform.position, transform.rotation);
        
        //If player has a nameplate, destroy it
        if (GetComponent<NameplateDisplay>() != null)
        {
            Destroy(nameplateDisplay.nameplate);
        }

        if (TryGetComponent<BallBomb>(out var bomb))
        {
            Destroy(bomb.timerText.transform.parent.gameObject);
        }

        Destroy(transform.parent.gameObject);
    }
}
