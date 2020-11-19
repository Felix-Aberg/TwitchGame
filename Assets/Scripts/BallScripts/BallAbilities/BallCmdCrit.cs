using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCmdCrit : BallCommand
{
    //How much the crit chance increments by from 0.0-1.0
    public float critChanceIncrement = 1f;

    public override void DoAbility()
    {
        GetComponent<BallCollision>().critChance += critChanceIncrement;
    }
}
