using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrip : MonoBehaviour
{
    public GameObject waterLevel;
    public float wLwaitTime;

    public GameObject droplett;

    public float minTimer;
    public float maxTimer;
    public float timer;

    public bool spawnWater;

    public float spawnTimerGoal;
    public float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTimer, maxTimer);

        spawnWater = false;

        spawnTimer = spawnTimerGoal;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            spawnWater = !spawnWater;
            timer = Random.Range(minTimer, maxTimer);

            Invoke("UpdateWaterLevel", wLwaitTime);
        }
        if (spawnWater == true)
        {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer <= 0f)
            {
            Instantiate(droplett, transform.position, transform.rotation);
                spawnTimer = spawnTimerGoal;
            }

        }
       
    }
    void UpdateWaterLevel()
    {
        waterLevel.GetComponent<WaterLevel>().isDripping = spawnWater;
    }
}
