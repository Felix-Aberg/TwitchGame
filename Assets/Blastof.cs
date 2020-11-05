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

    public float MinTimer;
    public float MaxTimer;
    [SerializeField]
    float timerr;

    public GameObject blasteroffer;

    // Start is called before the first frame update
    void Start()
    {
        timerr = Random.Range(MinTimer, MaxTimer);
        startpos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= startpos.y)
        {

            timerr -= Time.deltaTime;
            if(timerr <= 0)
            {
                go = true;

                timerr = Random.Range(MinTimer, MaxTimer);
            }
        }
        if(go == true)
        {
            blasteroffer.GetComponent<Collider>().enabled = true;
            transform.position = transform.position + new Vector3(0, 5 * upSpeed * Time.deltaTime, 0);

        }
        if(transform.position.y > targethight.y)
        {
            go = false;
        }
        if (go == false && transform.position.y > startpos.y)
        {
            blasteroffer.GetComponent<Collider>().enabled = false;

            transform.position = transform.position - new Vector3(0, 5 * downSpeed * Time.deltaTime, 0);
        }

        
    }
}
