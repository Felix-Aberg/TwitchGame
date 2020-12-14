using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorShooter : MonoBehaviour
{

    public GameObject beam;
    public Transform shootPoint;
    public Transform Turret;

    public float timer;
    public float minTimer;
    public float maxTimer;

   
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTimer, maxTimer);
     
    }

    // Update is called once per frame
    void Update()
    {

        if(timer <= 0)
        {
            Instantiate(beam, shootPoint.position, transform.rotation);
            timer = Random.Range(minTimer, maxTimer);
        }
        else
        {
        timer -= Time.deltaTime;
        }
    }
}
