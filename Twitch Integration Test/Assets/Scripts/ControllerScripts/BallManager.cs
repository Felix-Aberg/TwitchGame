using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    List<GameObject> balls;
    GameObject ballPrefab;

    private void Start()
    {
        ballPrefab = Resources.Load("Prefabs/Ball") as GameObject;
    }

    public void AddBall(string name)
    {
        balls[balls.Count] = Instantiate(ballPrefab);
        balls[balls.Count - 1].name = name;
        //Initialize unique values here? Return ball to be changed elsewhere? TBD

    }

    public void RemoveBall(GameObject obj)
    {
        balls.Remove(obj);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddBall(((int)Random.Range(0.0f, 100000.0f)).ToString());
        }
    }
}
