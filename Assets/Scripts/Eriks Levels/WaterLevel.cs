using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{

    public GameObject Tap;
    public WaterDrip water;

    public Vector3 targethight;
    public float speed;
    public float downSpeed;
   
   public Vector3 startpos;
  

  public bool isDripping;

    public GameObject theBoosters;

    // Start is called before the first frame update
    void Start()
    {
        water = Tap.GetComponent<WaterDrip>();


        startpos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

     


        if(isDripping == true && transform.position.y < targethight.y)
        {
            
            transform.position = transform.position + new Vector3(0, 1 * speed * Time.deltaTime, 0);

        }
        if (isDripping == false && transform.position.y > startpos.y)
        {

            transform.position = transform.position - new Vector3(0, 1 * downSpeed * Time.deltaTime, 0);

            theBoosters.SetActive(true);
        }
        else
            theBoosters.SetActive(false);
        
        
    }
}
