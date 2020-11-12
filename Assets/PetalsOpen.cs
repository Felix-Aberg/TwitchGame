using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalsOpen : MonoBehaviour
{
    public Vector3 targethight;
    public float smooth = 5f;

 
   public Vector3 startpos;
    public bool go;

    public float MinTimer;
    public float MaxTimer;
    [SerializeField]
    float timerr;

    

    // Start is called before the first frame update
    void Start()
    {
        targethight = new Vector3(transform.position.x, transform.position.y, targethight.z);

        timerr = Random.Range(MinTimer, MaxTimer);
       
        startpos = (transform.rotation.eulerAngles);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.z <= startpos.z)
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
           
            Quaternion target = Quaternion.Euler(targethight);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        }
        if(transform.rotation.z > targethight.z)
        {
            go = false;
        }
        if (go == false && transform.rotation.z >= startpos.z)
        {
            Quaternion target = Quaternion.Euler(startpos);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
           

            
        }

        
    }
}
