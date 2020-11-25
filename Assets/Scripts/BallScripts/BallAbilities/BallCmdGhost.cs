﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCmdGhost : BallCommand
{
    
    public float transparency = 0.2f;
    public bool ghostActive;
    private float timer = 8f;

    BallCollision ballCollision;

    public override void Init()
    {
        sprite = Resources.Load<Sprite>("Images/Icon/Ghost Icon");
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
        ToggleIcon(false);

        setAlpha(1f);

        gameObject.layer = 8;

        ghostActive = false;
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
            Debug.Log(child.material.color);

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