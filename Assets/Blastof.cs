using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastof : MonoBehaviour
{
    public Vector3 targethight;
    public float upSpeed;
    public float downSpeed;
   public Vector3 startpos;
    public bool go;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < startpos.y)
        {
            go = true;
        }
        if(go == true)
        {
        transform.position = transform.position + new Vector3(0, 5 * upSpeed * Time.deltaTime, 0);

        }
        if(transform.position.y > targethight.y)
        {
            go = false;
        }
        if (go == false)
        {
            transform.position = transform.position - new Vector3(0, 5 * downSpeed * Time.deltaTime, 0);
        }

        
    }
}
