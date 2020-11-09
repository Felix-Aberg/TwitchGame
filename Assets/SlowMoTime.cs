using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoTime : MonoBehaviour
{

    public float timeSlowdonwTime = 0.05f;
    public float timeSpeedUpLenght = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Time.timeScale += (1f / timeSpeedUpLenght) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);


       if (Input.GetKey(KeyCode.Z))
        {
            Time.timeScale = timeSlowdonwTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            /*if (Time.timeScale <= timeSlowGoal)
            {
                Time.timeScale = timeSlowGoal;
        }*/
            }
        
       
                
    }
}
