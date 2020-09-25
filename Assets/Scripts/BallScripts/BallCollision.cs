using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    Rigidbody rigidbody;
    BallRPM ballRPM;
    public ParticleSystem sparks;

    Vector3 direction;
    Vector3 finalForce;

    float magnitude;
    float RNGmultiplier;

    public float RPMmultiplier;
    public float velocityMultiplier;

    public float RNGminMultiplier;
    public float RNGmaxMultiplier;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        ballRPM = GetComponent<BallRPM>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ////Placeholder collision, push each other away by
            //velocity = rigidbody.velocity.magnitude;
            //push = transform.position - collision.transform.position;

            //push = push.normalized * pushForce;
            //RNGmultiplier = Random.Range(RNGminMultiplier, RNGmaxMultiplier);
            //finalForce = -push.normalized * velocity * RNGmultiplier;

            direction = collision.transform.position - transform.position;
            direction.Normalize();

            RNGmultiplier = Random.Range(RNGminMultiplier, RNGmaxMultiplier);

            magnitude = (rigidbody.velocity.magnitude * velocityMultiplier + ballRPM.RPM * RPMmultiplier) *
                         RNGmultiplier /* * RollCrit()*/;

            finalForce = direction * magnitude;

            collision.rigidbody.AddForce(finalForce);

            sparks.Play();

            //Magnitude = (Velocity + RPM(?)) * RollCrit() * RNG_variety
        }
    }
    
}
