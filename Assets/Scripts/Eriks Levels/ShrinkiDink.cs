using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkiDink : MonoBehaviour
{
    public Transform arena;
     Vector3 scaleChange;
    public float shrinkSpeed;
    public bool doShrink;

    public float timeUntilShrink;
    public float toosmol;

    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(-shrinkSpeed, -shrinkSpeed, -shrinkSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        if (doShrink == false)
        {
            timeUntilShrink -= Time.deltaTime;

            if (timeUntilShrink <= 0f)
            {
                doShrink = true;
            }
        }
        else
        {
            arena.localScale += scaleChange * Time.deltaTime;
            
            if(arena.localScale.x < toosmol)
            {
                Destroy(this);
            }

        }
    }
}
