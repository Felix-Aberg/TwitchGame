using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubble : MonoBehaviour
{

    public GameObject bubble;

    public float minTimer;
    public float maxTimer;
    public float timer;
    public Transform spawnPoin;
   public List<Transform> spwanPontList;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTimer, maxTimer);

        for(int i = 0; i< spawnPoin.childCount; i++)
        {
            spwanPontList.Add(spawnPoin.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        if(timer <= 0f)
        {


            Instantiate(bubble, spwanPontList[Random.Range(0, spwanPontList.Count - 1)]);

            timer = Random.Range(minTimer, maxTimer);
        }
    }
}
