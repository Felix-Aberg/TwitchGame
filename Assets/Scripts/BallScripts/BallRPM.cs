using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRPM : MonoBehaviour
{
    [HideInInspector] public float RPM;

    [Tooltip("Percent of RPM lost per second based off current RPM")]
    public float variableDrag;

    [Tooltip("RPM lost per second regardless of current RPM")]
    public float constantDrag;

    /*
    // RPM consistently draining
    private void Update()
    {
        RPM -= (RPM * variableDrag * 0.01f * Time.deltaTime) + (constantDrag * Time.deltaTime);
    }
    */
}
