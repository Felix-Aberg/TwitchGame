using System;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public BallConfig ballConfig;

    Rigidbody rb;
    BallRPM ballRPM;
    public ParticleSystem sparks;
    public ParticleSystem critSparks;

    public AudioSource audioSource;

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
    float critChance = 0f;
    [Tooltip("Multiplies force by this on crit")]
    float critMultiplier;
    bool doCrit = false;

    [Header("Debug")]
    [Tooltip("Will debug a message to the log if a collision's push force exceeds this value")]
    public float debugForceLimit;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballRPM = GetComponent<BallRPM>();

        //Initialise ballConfig values
    }

    public void InitializeConfig()
    {
        if (ballConfig == null)
        {
            Debug.Break();
            Debug.LogError("ERROR! Ballconfig is missing!");
        }
        GetComponent<BallRPM>().RPM = ballConfig.initRPM;
        critMultiplier = ballConfig.initCritMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            lastHitByName = collision.gameObject.name;
            lastHitByGameObject = collision.gameObject;
        }

        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Ball")
        {
            direction = collision.transform.position - transform.position;
            direction.Normalize();

            RNG_multiplier = UnityEngine.Random.Range(ballConfig.RNGMinMultiplier, ballConfig.RNGMaxMultiplier);

            doCrit = RollCrit();

            float RPM = Mathf.Clamp(ballRPM.RPM, 0, ballConfig.maxRPM);

            magnitude = (rb.velocity.magnitude * ballConfig.velocityMultiplier + RPM * ballConfig.RPMMultiplier)
                * RNG_multiplier;

            audioSource.pitch = UnityEngine.Random.Range(0.5f, 2.0f);
            audioSource.Play();


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
                + "<b>Crit:</b> " + doCrit
                + " <b>RPM push force:</b> " + (int)(ballRPM.RPM * ballConfig.RPMMultiplier)
                + " <b>Velocity push force:</b> " + (int)(rb.velocity.magnitude * ballConfig.velocityMultiplier));
            }

            finalForce = direction * magnitude;

            collision.rigidbody.AddForce(finalForce);

            ballRPM.RPM -= UnityEngine.Random.Range(ballConfig.RPMMinDamageOnHit, ballConfig.RPMMaxDamageOnHit);
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
