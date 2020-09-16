using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    //I am really not sure if I should exclusively be using a dictionary
    public Dictionary<string, GameObject> ballDictionary;


    GameObject ballPrefab;
    Transform parent;

    private void Start()
    {
        ballDictionary = new Dictionary<string, GameObject>();

        parent = Instantiate(new GameObject()).transform;
        parent.name = "Balls";
        ballPrefab = Resources.Load("Prefabs/Ball") as GameObject;
    }

    public void AddBall(string name)
    {
        if (!ballDictionary.ContainsKey(name))
        {
            //Create ball
            GameObject ball = Instantiate(ballPrefab);

            ball.transform.parent = parent;
            ball.name = name;

            //Add to dictionary
            ballDictionary.Add(name, ball);

            //TODO: Initialize unique values here? Return ball to be changed elsewhere?
            /*
            Color newColor = Random.ColorHSV(0.0f, 1f);
            
            Material newMaterial =  
            Debug.Log(newMaterial);
            newMaterial.SetColor("newColor", newColor);
            ball.GetComponent<MeshRenderer>().material = material;
            */
        }
    }

    public void RemoveBall(string name)
    {
        ballDictionary.Remove(name);
    }

    public void RemoveBall(GameObject obj)
    {
        ballDictionary.Remove(obj.name);
    }

    private void Update()
    {
        //Press spacebar to create a ball with a unique ID
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TEMP rng ball spawn
            AddBall(ballDictionary.Count.ToString());
        }
    }
}
