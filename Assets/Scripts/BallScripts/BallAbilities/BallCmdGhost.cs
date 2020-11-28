using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCmdGhost : BallCommand
{
    
    public float transparency = 0.2f;
    public bool ghostActive;
    private float timer = 8f;
    private float critTimer = 0.05f;
    private float ghostExitCritChance = 1f;

    BallCollision ballCollision;

    public override void Init()
    {
        sprite = Resources.Load<Sprite>("Images/Icon/Ghost Icon");
        ballCollision = GetComponent<BallCollision>();
    }

    public override void DoAbility()
    {
        ghostActive = true;

        gameObject.layer = 11;
        setAlpha(transparency);

        Invoke("DisableGhost", timer);
    }

    private void DisableGhost()
    {

        ballCollision.critChance += ghostExitCritChance;
        ToggleIcon(false);

        setAlpha(1f);

        gameObject.layer = 8;

        ghostActive = false;

        Invoke("DisableCrits", critTimer);
    }

    private void DisableCrits()
    {
        ballCollision.critChance -= ghostExitCritChance;
    }

    public void setAlpha(float alpha)
    {
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        Color newColor;
        foreach (MeshRenderer child in children)
        {
            newColor = child.material.color;
            newColor.a = alpha;
            child.material.color = newColor;

            if(alpha == 1f)
            {
                MaterialExtensions.ToOpaqueMode(child.material);
            }
            else
            {
                MaterialExtensions.ToFadeMode(child.material);
            }
        }
    }
}
