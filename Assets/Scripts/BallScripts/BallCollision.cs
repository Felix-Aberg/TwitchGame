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
    float RNG_multiplier;

    public float RPM_multiplier;
    public float velocityMultiplier;

    public float RNG_minMultiplier;
    public float RNG_maxMultiplier;

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

            RNG_multiplier = Random.Range(RNG_minMultiplier, RNG_maxMultiplier);

            magnitude = (rigidbody.velocity.magnitude * velocityMultiplier + ballRPM.RPM * RPM_multiplier) *
                         RNG_multiplier /* * RollCrit()*/;

            if (magnitude > debugForceLimit)
            {
                Debug.Log("Collision occured with a force exceeding " + debugForceLimit + ", with a power of : " + magnitude);
            }

            finalForce = direction * magnitude;

            collision.rigidbody.AddForce(finalForce);

            sparks.Play();

            //Magnitude = (Velocity + RPM(?)) * RollCrit() * RNG_variety
        }
    }
    
}
