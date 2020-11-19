using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallCommand : MonoBehaviour
{
    public bool used;

    //public image etc

    public virtual void ActivateCommand()
    {
        //Do things like render icon



        DoAbility();
    }

    /// <summary>
    /// Please call ActivateCommand instead, this one is to be called by ActivateCommand();
    /// </summary>
    public abstract void DoAbility();

}
