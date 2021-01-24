using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCmdGrow : BallCommand 

{
    public float scaleX = 0.8f;
    public float scaleY = 0.8f;
    public float scaleZ = 0.8f;
    public float reScaleX = -0.8f;
    public float reScaleY = -0.8f;
    public float reScaleZ = -0.8f;

    public float mass;
    public float drag;
    public Rigidbody rb;

    public bool growActive;
    private float timer = 10f;


    BallCollision ballCollision;

    public override void Init()
    {
        sprite = Resources.Load<Sprite>("Images/Icon/Grow Icon");
        ballCollision = GetComponent<BallCollision>();
    }

    public override void DoAbility()
    {
        growActive = true;
        gameObject.transform.localScale += new Vector3(scaleX, scaleY, scaleZ);
    

        rb = GetComponent<Rigidbody>();
        rb.mass = 3f;
        rb.drag = 1f;
        
       
        gameObject.layer = 13;
       
      
        Invoke("DisableGrow", timer);
    }

    private void DisableGrow()
    {
        gameObject.transform.localScale += new Vector3(reScaleX, reScaleX, reScaleX);

        rb = GetComponent<Rigidbody>();
        rb.mass = 1f;
        rb.drag = 0f;
        gameObject.layer = 8;
      
        growActive = false;

        SelfDestruct();
    }
}
