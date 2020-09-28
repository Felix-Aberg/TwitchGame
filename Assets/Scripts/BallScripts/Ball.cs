using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    BallRPM ballRPM;
    public float minimumRPM;

    // Start is called before the first frame update
    void Start()
    {
        ballRPM = GetComponent<BallRPM>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -50f)
        {
            SelfDestruct();
        }

        if (ballRPM.RPM < minimumRPM)
        {
            SelfDestruct();
        }


    }

    void SelfDestruct()
    {
        //TODO: explode
        Destroy(gameObject);
    }
}
