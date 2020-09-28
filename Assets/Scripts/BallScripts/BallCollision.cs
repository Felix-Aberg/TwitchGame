using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    Rigidbody rigidbody;
    BallRPM ballRPM;
    public ParticleSystem sparks;
    public ParticleSystem critSparks;

    Vector3 direction;
    Vector3 finalForce;

    float magnitude;
    float RNG_multiplier;

    public float RPM_multiplier;
    public float velocityMultiplier;

    public float RNG_minMultiplier;
    public float RNG_maxMultiplier;


    [Tooltip("Multiplies force by this on crit")]
    public float critMultiplier;
    [Tooltip("In percent from 0.0 to 1.0")]
    public float critChanceIncrement;
    float critChance;
    bool doCrit = false;

    [Tooltip("Will debug a message to the log if a collision's push force exceeds this value")]
    public float debugForceLimit;


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

            RNG_multiplier = UnityEngine.Random.Range(RNG_minMultiplier, RNG_maxMultiplier);

            doCrit = RollCrit();

            magnitude = (rigidbody.velocity.magnitude * velocityMultiplier + ballRPM.RPM * RPM_multiplier)
                * RNG_multiplier;

            

            if (doCrit)
            {
                magnitude *= critMultiplier;

                critSparks.Play();
            }
            else
            {
                sparks.Play();
            }

            if (magnitude > debugForceLimit)
            {
                Debug.Log("<b>Excessive force!</b> <i>click for more info</i>" + Environment.NewLine
                + "<b>Crit:</b> " + doCrit + " <b>RPM push force:</b> " + ballRPM.RPM * RPM_multiplier + " <b>Velocity push force:</b> " + rigidbody.velocity.magnitude * velocityMultiplier);
            }

            finalForce = direction * magnitude;

            collision.rigidbody.AddForce(finalForce);

            

            //Magnitude = (Velocity + RPM(?)) * RollCrit() * RNG_variety
        }
    }

    bool RollCrit()
    {
        critChance += critChanceIncrement;
        //if crit
        if (UnityEngine.Random.Range(0f, 1f) < critChance)
        {
            critChance = 0f;
            return true;
        }
        return false;
    }

    
}
