using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalsOpen : MonoBehaviour
{
    public float targetHeight;
    public float smooth = 5f;

    public float startHeight;
    public bool ascending;

    public float minTimer;
    public float maxTimer;
    [SerializeField]
    float timer;

    

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTimer, maxTimer);
       
        startHeight = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ascending && transform.rotation.eulerAngles.y <= startHeight)
        {
            timer -= Time.fixedDeltaTime;
            
            //If time is up
            if(timer <= 0)
            {
                ascending = true;

                timer = Random.Range(minTimer, maxTimer);
            }
        }
        
        if (ascending == true)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.y = Mathf.Lerp(transform.rotation.eulerAngles.y, targetHeight, smooth * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(rot);
        }
        
        if (transform.rotation.eulerAngles.y > targetHeight)
        {
            ascending = false;
        }

        if (ascending == false && transform.rotation.eulerAngles.y >= startHeight)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.y = Mathf.Lerp(transform.rotation.eulerAngles.y, startHeight, smooth * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(rot);
        }
        
    }
}
