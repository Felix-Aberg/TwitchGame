using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This ability is the bomb spawned by the ball ability 
/// </summary>
public class BallBomb : MonoBehaviour
{
    string originPlayer;
    float explosionTimer;
    float explosionDamage;
    float explosionRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Pass the values from the previous bomb script
    /// </summary>
    void PassValues(float timer, string origin)
    {
        explosionTimer = timer;
        originPlayer = origin;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        explosionTimer -= Time.fixedDeltaTime;
        if (explosionTimer < 0f)
        {
            Explode();
        }
    }

    void Explode()
    {
        
    }
}
