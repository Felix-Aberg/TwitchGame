using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByTime : MonoBehaviour
{
    public float timer;

    public bool myowntime;
    public bool hasParticle;
    public GameObject Particle;
    // Start is called before the first frame update
    void Start()
    {

        if (myowntime == false)
        {
        timer = 2.1f;

        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            if(hasParticle == true)
            {
                Instantiate(Particle, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
