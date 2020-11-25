using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCmdCrit : BallCommand
{
    //How much the crit chance increments by from 0.0-1.0
    public float critChanceIncrement = 0.4f;

    BallCollision ballCollision;

    public override void Init()
    {
        sprite = Resources.Load<Sprite>("Images/Icon/Crit Icon");
    }

    public override void DoAbility()
    {
        ballCollision = GetComponent<BallCollision>();
        ballCollision.critChance += critChanceIncrement;
    }

    private void Update()
    {
        if (!iconDisabled
            && ballCollision != null 
            && ballCollision.critChance < critChanceIncrement)
        {
            ToggleIcon(false);
        }
    }
}
