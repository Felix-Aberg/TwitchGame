using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    Rigidbody rb;
    BallRPM ballRPM;
    BallConfig cfg;

    public bool extraGravityIsEnabled;
    public float gravityModifier;

    public float dir;
    public float circleSpeed;

    Transform child;
    Transform rotation;

    Vector3 childPosition;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        rotation = transform.parent.GetChild(1);

        rb = GetComponent<Rigidbody>();
        ballRPM = GetComponent<BallRPM>();
        cfg = GetComponent<BallCollision>().ballConfig;
        childPosition = child.localPosition;
        rb.centerOfMass = Vector3.zero;

        circleSpeed = Random.Range(cfg.minCircleSpeed, cfg.maxCircleSpeed);
        dir = Random.Range(0f, 360f);
        if (Random.value < 0.5f)
            circleSpeed *= -1;

        NewCircular();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (extraGravityIsEnabled)
        {
            rb.AddForce(Vector3.down * (gravityModifier - 1) * Time.fixedDeltaTime);
        }
        MoveCircular();
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

    void MoveCircular() 
    {
        dir += circleSpeed * Time.fixedDeltaTime;
        
        rotation.rotation = Quaternion.AngleAxis(dir, Vector3.up);
        Debug.Log(dir + " | " + rotation.rotation.eulerAngles);
        rb.AddForce(rotation.forward * cfg.circleForce * Time.fixedDeltaTime, ForceMode.Force);
    }

    public void NewCircular()
    {
        circleSpeed = Random.Range(cfg.minCircleSpeed, cfg.maxCircleSpeed);
        dir = Random.Range(0f, 360f);
        if (Random.value < 0.5f)
            circleSpeed *= -1;
    }
}
