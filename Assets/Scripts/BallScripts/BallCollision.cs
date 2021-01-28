using System;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public BallConfig ballConfig;

    Rigidbody rb;
    BallDurability ballDur;
    BallPhysics ballPhysics;
    NameplateDisplay nameplate;
    
    public ParticleSystem sparks;
    public ParticleSystem critSparks;
    public bool hasBomb;

    public AudioSource audioSource;
    public AudioClip audioHit;
    public AudioClip audioCrit;

    public string lastHitByName; //Used to determine killfeeds
    public GameObject lastHitByGameObject; //Used to give RPM

    Vector3 direction;
    Vector3 finalForce;

    float magnitude;
    float RNG_multiplier;

    public bool superCritEnabled;

    /* Most variables are stored in BallConfig ScriptableObject
     * Which BallConfig file that is used is decided by the BallManager's GameController
     * 
     * All the remaining ""config"" variables (below) are ones that get changed on a object-to-object basis in runtime.
     */

    public float critChance = 0f;
    [Tooltip("Multiplies force by this on crit")]
    float critMultiplier;
    bool doCrit = false;

    [Header("Debug")]
    [Tooltip("Will debug a message to the log if a collision's push force exceeds this value")]
    public float debugForceLimit;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballDur = GetComponent<BallDurability>();
        ballPhysics = GetComponent<BallPhysics>();
        nameplate = GetComponent<NameplateDisplay>();

        //Initialise ballConfig values
    }

    public void InitializeConfig()
    {
        if (ballConfig == null)
        {
            Debug.Break();
            Debug.LogError("ERROR! Ballconfig is missing!");
        }
        GetComponent<BallDurability>().RPM = ballConfig.initDur;
        critMultiplier = ballConfig.initCritMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /* 
        if (TryGetComponent<BallCmdGhost>(out BallCmdGhost ghost))
        {
            if (ghost.ghostActive)
                return;
        }

        if (collision.gameObject.TryGetComponent<BallCmdGhost>(out BallCmdGhost theirGhost))
        {
            if (theirGhost.ghostActive)
                return;             
        }
        */

        if (collision.gameObject.tag == "Ball")
        {
            lastHitByName = collision.transform.parent.name;
            lastHitByGameObject = collision.gameObject;
            doCrit = RollCrit();

            //Bomb check
            if (hasBomb)
            {
                BallBomb oldBomb = GetComponent<BallBomb>();

                if (oldBomb.cooldown < 0 && collision.gameObject.GetComponent<BallBomb>() == null)
                {
                    if(gameObject.TryGetComponent<BallCmdBomb>(out var component))
                    {
                        Debug.Log("Destroyed bomb command!");
                        component.SelfDestruct();
                    }

                    BallBomb newBomb = collision.gameObject.AddComponent<BallBomb>();
                    newBomb.PassValues(oldBomb.explosionTimer, oldBomb.originPlayer, oldBomb.timerText);
                    Destroy(oldBomb);
                }
            }

            HitEnemy(collision);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            doCrit = false;
            HitEnemy(collision);
        }

        if (gameObject.tag == "Ball")
        {
            nameplate.ColorName(ballDur.RPM);
        }
    }

    void HitEnemy(Collision collision)
    {
        direction = collision.transform.position - transform.position;
        direction.Normalize();

        RNG_multiplier = UnityEngine.Random.Range(ballConfig.RNGMinMultiplier, ballConfig.RNGMaxMultiplier);

        float RPM = Mathf.Clamp(ballDur.RPM, 0, ballConfig.maxRPM);

        //Magnitude formula
        magnitude = (rb.velocity.magnitude * ballConfig.velocityMultiplier + 1200 * ballConfig.RPMMultiplier)
            * RNG_multiplier;

      

        if (doCrit)
        {
            if (superCritEnabled)
            {
                magnitude *= ballConfig.superCritMultiplier;
            }
            else
            {
                magnitude *= critMultiplier;
            }

            if (critSparks != null)
                critSparks.Play();

            audioSource.clip = audioCrit;
            audioSource.pitch = 1.0f;
        }
        else
        {
            if (sparks != null)
                sparks.Play();

            audioSource.clip = audioHit;
            audioSource.pitch = UnityEngine.Random.Range(0.5f, 2.0f);
        }

        if (audioSource != null)
        {
            
            audioSource.Play();
        }

        if (magnitude > debugForceLimit)
        {
            /*
            Debug.Log("<b>Excessive force</b> (" + (int)magnitude + ")" + Environment.NewLine
            + "<b>Crit:</b> " + doCrit
            + " <b>RPM push force:</b> " + (int)(ballRPM.RPM * ballConfig.RPMMultiplier)
            + " <b>Velocity push force:</b> " + (int)(rb.velocity.magnitude * ballConfig.velocityMultiplier));

            //*/
        }

        if (ballPhysics != null)
            ballPhysics.NewCircular();

        finalForce = direction * magnitude;

        collision.rigidbody.AddForce(finalForce);

        if (gameObject.layer != 13) //layer 13 "Invulnerable", used for !grow
            ballDur.RPM -= UnityEngine.Random.Range(ballConfig.DurMinDamageOnHit, ballConfig.DurMaxDamageOnHit);
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
