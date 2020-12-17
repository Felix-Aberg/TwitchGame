using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCmdBomb : BallCommand
{
    public override void Init()
    {
        sprite = Resources.Load<Sprite>("Images/Icon/Bomb Icon");
    }
    public override void DoAbility()
    {
        // Instantiate bomb
        BallBomb ballBomb = gameObject.AddComponent<BallBomb>();
        ballBomb.originPlayer = gameObject.name;

        GameObject obj = Resources.Load("Prefabs/Gameplay prefabs/BombDisplay") as GameObject;
        Transform trans = GameObject.FindGameObjectWithTag("PowerupCanvas").transform;
        
        ballBomb.timerText = Instantiate(obj, trans).GetComponentInChildren<Text>();

    }


    // Things I need:
    // Bomb itself
    // UI icon
    // Kill credit
}
