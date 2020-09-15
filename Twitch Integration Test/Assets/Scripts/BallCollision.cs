using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public float PushForce;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            //Placeholder collision, push each other away by
            //Vector2.Angle(transform.position, collision.transform.position);
        }
    }
}
