using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expanding : MonoBehaviour
{

    public GameObject shpere;
    public Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        shpere.transform.localScale += scaleChange;
    }
}
