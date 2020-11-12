using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blastof : MonoBehaviour
{
    public Vector3 targethight;
    public float upSpeed;
    public float downSpeed;
   public Vector3 startpos;
    public bool ascending;

    public float minTimer;
    public float maxTimer;
    [SerializeField]
    float timer;

    public GameObject blasteroffer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTimer, maxTimer);
        startpos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= startpos.y)
        {

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                ascending = true;

                timer = Random.Range(minTimer, maxTimer);
            }
        }
        if(ascending == true)
        {
            blasteroffer.GetComponent<Collider>().enabled = true;
            transform.position = transform.position + new Vector3(0, 5 * upSpeed * Time.deltaTime, 0);

        }
        if(transform.position.y > targethight.y)
        {
            ascending = false;
        }
        if (ascending == false && transform.position.y > startpos.y)
        {
            blasteroffer.GetComponent<Collider>().enabled = false;

            transform.position = transform.position - new Vector3(0, 5 * downSpeed * Time.deltaTime, 0);
        }

        
    }
}
