using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterTrack : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 direction;
    public float boostSpeed;
    public float divideSpinnerVelocity = 2f;
    public GameObject booster;
    // Start is called before the first frame update
    void Start()
    {
       direction = booster.transform.right;
    }
    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            rb = other.GetComponent<Rigidbody>();

            rb.velocity = rb.velocity / divideSpinnerVelocity - direction * boostSpeed;

        }
        else
            return;
    }
}
