using System;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public BallConfig ballConfig;

    Rigidbody rb;
    BallRPM ballRPM;
    public ParticleSystem sparks;
    public ParticleSystem critSparks;

    public string lastHitByName; //Used to determine killfeeds
    public GameObject lastHitByGameObject; //Used to give RPM

    Vector3 direction;
    Vector3 finalForce;

    float magnitude;
    float RNG_multiplier;

    /* Most variables are stored in BallConfig ScriptableObject
     * Which BallConfig file that is used is decided by the BallManager's GameController
     * 
     * All the remaining ""config"" variables (below) are ones that get changed on a object-to-object basis in runtime.
     */

    [Header("RNG")]
    public float RNG_minMultiplier;
    public float RNG_maxMultiplier;

    [Header("Damage values")]
    public float RPM_minDagameOnHit;
    public float RPM_maxDagameOnHit;
    public float RPM_onKill;
    public float HP_dagameOnHit; //not yet used

    [Tooltip("Multiplies force by this on crit")]
    public float critMultiplier;
    [Tooltip("In percent from 0.0 to 1.0")]

    
    float critChance = 0f;
    bool doCrit = false;

    [Tooltip("Will debug a message to the log if a collision's push force exceeds this value")]
    public float debugForceLimit;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballRPM = GetComponent<BallRPM>();
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ball")
        {
            lastHitByName = collision.gameObject.name;
            lastHitByGameObject = collision.gameObject;

            direction = collision.transform.position - transform.position;
            direction.Normalize();

            RNG_multiplier = UnityEngine.Random.Range(RNG_minMultiplier, RNG_maxMultiplier);

            doCrit = RollCrit();

            float RPM = Mathf.Clamp(ballRPM.RPM, 0, ballConfig.maxRPM);

            magnitude = (rb.velocity.magnitude * ballConfig.velocityMultiplier + RPM * ballConfig.RPMMultiplier)
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
                + " <b>RPM push force:</b> " + (int)(ballRPM.RPM * ballConfig.RPMMultiplier) 
                + " <b>Velocity push force:</b> " + (int)(rb.velocity.magnitude * ballConfig.velocityMultiplier));
            }

            finalForce = direction * magnitude;

            collision.rigidbody.AddForce(finalForce);

            ballRPM.RPM -= UnityEngine.Random.Range(RPM_minDagameOnHit, RPM_maxDagameOnHit);
            //TODO: Reduce self HP;
        }
    }

    bool RollCrit()
    {
        critChance += ballConfig.critChanceIncrement;
        //if crit
        if (UnityEngine.Random.Range(0f, 1f) < critChance)
        {
            critChance = 0f;
            critMultiplier += ballConfig.critMultiplierIncrement;
            return true;
        }
        return false;
    }

    
}
