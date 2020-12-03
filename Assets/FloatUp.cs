using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUp : MonoBehaviour
{

    private float floatSpeed;

    public float speedMax;
    public float speedMin;
    // Start is called before the first frame update
    void Start()
    {
        floatSpeed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,floatSpeed,0) * Time.deltaTime;  
    }
}
