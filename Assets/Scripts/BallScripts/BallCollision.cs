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

    public float RPM_dagameOnHit;
    public float HP_dagameOnHit; //not yet used

    public float maxRPM;



    [Tooltip("Multiplies force by this on crit")]
    public float critMultiplier;
    public float critMultiplierIncrement;
    [Tooltip("In percent from 0.0 to 1.0")]
    public float critChanceIncrement;
    public float critChance;
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

            float RPM = Mathf.Clamp(ballRPM.RPM, 0, maxRPM);

            magnitude = (rigidbody.velocity.magnitude * velocityMultiplier + RPM * RPM_multiplier)
                * RNG_multiplier;


            if (doCrit)
            {
                magnitude *= critMultiplier;

                if (critSparks != null)
                    critSparks.Play();

            }
            else
            {
                if (sparks != null)
                    sparks.Play();
            }

            if (magnitude > debugForceLimit)
            {
                Debug.Log("<b>Excessive force</b> (" + (int)magnitude + ")" + Environment.NewLine
                +  "<b>Crit:</b> " + doCrit 
                + " <b>RPM push force:</b> " + (int)(ballRPM.RPM * RPM_multiplier) 
                + " <b>Velocity push force:</b> " + (int)(rigidbody.velocity.magnitude * velocityMultiplier));
            }

            finalForce = direction * magnitude;

            collision.rigidbody.AddForce(finalForce);

            ballRPM.RPM -= RPM_dagameOnHit;
            //TODO: Reduce self HP;
        }
    }

    bool RollCrit()
    {
        critChance += critChanceIncrement;
        //if crit
        if (UnityEngine.Random.Range(0f, 1f) < critChance)
        {
            critChance = 0f;
            critMultiplier += critMultiplierIncrement;
            return true;
        }
        return false;
    }

    
}
