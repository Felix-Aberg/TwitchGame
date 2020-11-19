using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforceup : MonoBehaviour
{
    public float launchPower;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(0, launchPower, 0, ForceMode.Impulse);
        }
    }
}
