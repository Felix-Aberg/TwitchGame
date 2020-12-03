using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    Rigidbody rb;
    BallDurability ballDur;
    BallConfig cfg;

    public bool extraGravityIsEnabled;
    public float gravityModifier;

    public float dir;
    public float circleSpeed;
    float circleForce;

    Transform child;
    Transform rotation;

    Vector3 childPosition;

    int layer;
    int mapCollisions;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        rotation = transform.parent.GetChild(1);

        rb = GetComponent<Rigidbody>();
        ballDur = GetComponent<BallDurability>();
        cfg = GetComponent<BallCollision>().ballConfig;
        childPosition = child.localPosition;
        rb.centerOfMass = Vector3.zero;

        layer = LayerMask.NameToLayer("Map");

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

        circleForce -= cfg.decaySpeed * Time.fixedDeltaTime;

        //If colliding with anything on the map layer
        if (mapCollisions > 0 && circleForce > 0)
        {
            MoveCircular();
        }
    }

    void LateUpdate()
    {
        Vector3 deltaRotation = child.transform.rotation.eulerAngles;
        deltaRotation.x = 0f;
        deltaRotation.y += Time.deltaTime * 2500;
        deltaRotation.z = 0f;

        child.transform.rotation = Quaternion.Euler(deltaRotation);
        child.position = transform.position + childPosition;
    }

    void MoveCircular() 
    {
        dir += circleSpeed * Time.fixedDeltaTime;
        
        rotation.rotation = Quaternion.AngleAxis(dir, Vector3.up);
        rb.AddForce(rotation.forward * circleForce * Time.fixedDeltaTime, ForceMode.Force);
    }

    public void NewCircular()
    {
        circleForce = cfg.circleForce;
        circleSpeed = Random.Range(cfg.minCircleSpeed, cfg.maxCircleSpeed);
        dir = Random.Range(0f, 360f);
        if (Random.value < 0.5f)
            circleSpeed *= -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == layer)
        {
            mapCollisions++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == layer)
        {
            mapCollisions--;
        }
    }
}
