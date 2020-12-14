using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCmdBomb : BallCommand
{
    public override void DoAbility()
    {

    }

    public override void Init()
    {
        sprite = Resources.Load<Sprite>("Images/Icon/Ghost Icon");
    }

    // Things I need:
    // Bomb itself
    // UI icon
    // Kill credit

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
