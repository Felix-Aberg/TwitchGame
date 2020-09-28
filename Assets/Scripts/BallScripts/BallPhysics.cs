using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    Rigidbody rigidbody;
    BallRPM ballRPM;

    public bool extraGravityIsEnabled;
    public float gravityModifier;

    

    Transform child;

    Vector3 childPosition;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        rigidbody = GetComponent<Rigidbody>();
        ballRPM = GetComponent<BallRPM>();

        childPosition = child.localPosition;
        rigidbody.centerOfMass = Vector3.zero;
        TurnClockwise(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (extraGravityIsEnabled)
        {
            rigidbody.AddForce(Vector3.down * (gravityModifier - 1));
        }
    }
    void LateUpdate()
    {
        Vector3 deltaRotation = child.transform.rotation.eulerAngles;
        deltaRotation.x = 0f;
        deltaRotation.y += ballRPM.RPM * Time.deltaTime * 5;
        deltaRotation.z = 0f;

        child.transform.rotation = Quaternion.Euler(deltaRotation);
        child.position = transform.position + childPosition;
    }

    void TurnClockwise(Vector3 center)
    {

    }
}
