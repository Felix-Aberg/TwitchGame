using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
    public float radius;
    public float force = 700f;
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
    void Explode()
    {
        Collider[] colids = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colids)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
