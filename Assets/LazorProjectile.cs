using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorProjectile : MonoBehaviour
{
    public float speed = 80;
    public GameObject lazorExplotion;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0f, 90f, 0f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * Vector3.left * Time.deltaTime * speed;

    }
    private void OnTriggerEnter(Collider other)
    {
        
        Instantiate(lazorExplotion, transform);
        
        Debug.Log("hit");
        //Destroy(gameObject);
    }
    
}
