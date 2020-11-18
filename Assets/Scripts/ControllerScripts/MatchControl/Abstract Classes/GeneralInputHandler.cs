using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralInputHandler : ScriptableObject
{
    public abstract void Exit();
    public abstract void Continue();

    /// <summary>
    /// This function allows optional implementation of input keys not used across scenes universally, such as SHIFT + B
    /// </summary>
    public virtual void OtherInputs()
    {
    }
}