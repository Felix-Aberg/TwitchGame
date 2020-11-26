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

    public float minTimer;
    public float maxTimer;
    [SerializeField]
    float timerr;

    public GameObject blasteroffer;


    [SerializeField] bool specificStart;
    public float specificStartTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (specificStart == true)
        { 
            timerr = specificStartTimer;
        }else
        {
        timerr += Random.Range(minTimer, maxTimer);

        }

        startpos = transform.localPosition;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (specificStart == true)
        {

            if (transform.localPosition.y <= startpos.y)
            {
                transform.localPosition = transform.localPosition;
            }
            timerr -= Time.fixedDeltaTime;
            if (timerr <= 0)
            {
                go = true;

                timerr += Random.Range(minTimer, maxTimer);
            }
        }
        else
        {
        if(transform.localPosition.y <= startpos.y)
        {

            timerr -= Time.fixedDeltaTime;
            if(timerr <= 0)
            {
                go = true;

                timerr += Random.Range(minTimer, maxTimer);
            }
        }

        }
        if(go == true)
        {
            blasteroffer.GetComponent<Collider>().enabled = true;
            transform.position += new Vector3(0, 5 * upSpeed * Time.fixedDeltaTime, 0);

        }
        if(transform.localPosition.y > targethight.y)
        {
            go = false;
        }
        if (go == false && transform.localPosition.y > startpos.y)
        {
            blasteroffer.GetComponent<Collider>().enabled = false;

            transform.localPosition -= new Vector3(0, 5 * downSpeed * Time.fixedDeltaTime, 0);
        }

        
    }
}
