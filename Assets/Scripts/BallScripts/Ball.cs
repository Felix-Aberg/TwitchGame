using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isBot;
    public GameObject matchController;
    BallRPM ballRPM;
    BallCollision ballCollision;
    NameplateDisplay nameplateDisplay;

    public float minimumRPM;
    public GameObject deathParticles;

    // Start is called before the first frame update
    void Start()
    {
        name = transform.parent.name;
        ballRPM = GetComponent<BallRPM>();
        ballCollision = GetComponent<BallCollision>();
        nameplateDisplay = GetComponent<NameplateDisplay>();
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
        if(matchController == null)
        {
            Debug.LogWarning("Ball didn't find MatchController when instantiating. This is 100% unintended for normal gameplay.");
            matchController = GameObject.FindGameObjectsWithTag("MatchController")[0];
            if (matchController == null)
            {
                Debug.LogError("Ball didn't find MatchController when searching for it!. This is 100% unintended at any point ever!");
            }
        }

        matchController.GetComponent<KillFeed>().PostKill(transform.parent.name, ballCollision.lastHitByName);
        matchController.GetComponent<PlayerCount>().RemovePlayer();

        if(!isBot)
        {
            //Statistics: Add death to player
            matchController.GetComponent<DataManager>().playerSessionDataArray[name].deaths += 1;
            matchController.GetComponent<DataManager>().playerTotalDataArray[name].deaths += 1;
        }
        
        if (ballCollision.lastHitByGameObject != null)
        {
            //Gain RPM on kill
            ballCollision.lastHitByGameObject.GetComponent<BallRPM>().RPM += ballCollision.ballConfig.RPMOnKill;

            if (!ballCollision.lastHitByGameObject.GetComponent<Ball>().isBot)
            {
                if (isBot)
                {
                    //Statistics: Add bot kill to player
                    matchController.GetComponent<DataManager>().playerSessionDataArray[ballCollision.lastHitByName].botKills += 1;
                    matchController.GetComponent<DataManager>().playerTotalDataArray[ballCollision.lastHitByName].botKills += 1;
                }
                else
                {
                    //Statistics: Add kill to player
                    matchController.GetComponent<DataManager>().playerSessionDataArray[ballCollision.lastHitByName].kills += 1;
                    matchController.GetComponent<DataManager>().playerTotalDataArray[ballCollision.lastHitByName].kills += 1;
                }

                PlayerCount playerCount = matchController.GetComponent<PlayerCount>();
                if (playerCount.totalPlayers == playerCount.alivePlayers + 1)
                {
                    //Statistics: Add bounty kill to player
                    matchController.GetComponent<DataManager>().playerSessionDataArray[ballCollision.lastHitByName].firstBloods += 1;
                    matchController.GetComponent<DataManager>().playerTotalDataArray[ballCollision.lastHitByName].firstBloods += 1;
                }
            }
        }
        
        Instantiate(deathParticles, transform.position, transform.rotation);
        
        //If player has a nameplate, destroy it
        if (GetComponent<NameplateDisplay>() != null)
        {
            Destroy(nameplateDisplay.nameplate);
        }

        Destroy(transform.parent.gameObject);
    }
}
