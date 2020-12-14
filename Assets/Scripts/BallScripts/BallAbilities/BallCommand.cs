using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BallCommand : MonoBehaviour
{
    public bool used;
    public bool iconDisabled;

    public Sprite sprite;
    public Image image;
    public Ball ball;

    //Init checklist
    //-Load sprite
    public abstract void Init();
    public abstract void DoAbility();

    public virtual void ActivateCommand()
    {
        ball = GetComponent<Ball>();
        ball.abilityActive = true;
        Init();
        if (sprite == null)
        {
            Debug.Log("Sprite is null after initialisation!");
        }
        else
        {
            image = GetComponent<NameplateDisplay>().nameplate.GetComponentInChildren<Image>(true);
            image.sprite = sprite;
            image.enabled = true;
        }

        DoAbility();

        used = true;
    }

    public virtual void SelfDestruct()
    {
        GetComponent<NameplateDisplay>().nameplate.GetComponentInChildren<Image>(true).enabled = false;
        image.enabled = false;
        ball.abilityActive = false;
        ball.abilityCharges--;
        Destroy(this);
    }
}
