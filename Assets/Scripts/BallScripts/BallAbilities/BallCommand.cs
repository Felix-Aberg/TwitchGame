using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BallCommand : MonoBehaviour
{
    public bool used;
    public bool iconDisabled;

    public Sprite sprite;

    //Init checklist
    //-Load sprite
    public abstract void Init();
    public abstract void DoAbility();

    public virtual void ActivateCommand()
    {
        Init();
        if (sprite == null)
        {
            Debug.Log("Sprite is null after initialisation!");
        }
        else
        {
            GetComponent<NameplateDisplay>().nameplate.GetComponentInChildren<Image>(true).sprite = sprite;
            ToggleIcon(true);
        }

        DoAbility();

        used = true;
    }

    public void ToggleIcon(bool enable)
    {
        GetComponent<NameplateDisplay>().nameplate.GetComponentInChildren<Image>(true).enabled = enable;
    }

    /// <summary>
    /// Don't call this
    /// </summary>
    public virtual void SelfDestruct()
    {
        ToggleIcon(false);
        Destroy(this);
    }
}
