using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Delete), delay);
    }

    // Update is called once per frame
    void Delete()
    {
        Destroy(gameObject);
    }
}
