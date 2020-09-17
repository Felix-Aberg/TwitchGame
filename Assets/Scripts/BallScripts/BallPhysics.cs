using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    Rigidbody rigidbody;

    public bool extraGravityIsEnabled;
    public float gravityModifier;

    public bool magnetismIsEnabled;
    public float magnetModifier;
    public float maxDistance;
    Transform magnetTransform;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        magnetTransform = FindObjectOfType<GameController>().transform.Find("Magnet");
        if (magnetTransform == null)
        {
            Debug.LogError("Magnet transform is missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (extraGravityIsEnabled)
        {
            rigidbody.AddForce(Vector3.down * gravityModifier);
        }

        if (magnetismIsEnabled && Vector3.Distance(transform.position, magnetTransform.position) < maxDistance)
        {
            Vector3 appliedForce;
            appliedForce = (magnetTransform.position - transform.position) * magnetModifier;
            rigidbody.AddForce(appliedForce);
        }
    }
}
