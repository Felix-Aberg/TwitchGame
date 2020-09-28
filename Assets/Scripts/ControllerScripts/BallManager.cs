using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BallManager : MonoBehaviour
{
    //I am really not sure if I should exclusively be using a dictionary
    public Dictionary<string, GameObject> ballDictionary;
    public Dictionary<string, Material> materialDictionary;


    GameObject ballPrefab;
    Transform parent;
    int spawnPointAmount;
    Transform spawnPointTransform;


    private void Start()
    {
        ballDictionary = new Dictionary<string, GameObject>();
        materialDictionary = new Dictionary<string, Material>();

        parent = Instantiate(new GameObject()).transform;
        parent.name = "Balls";
        ballPrefab = Resources.Load("Prefabs/Ball") as GameObject;

        spawnPointTransform = transform.Find("SpawnPoints");
        spawnPointAmount = spawnPointTransform.childCount;

        //Add materials to dictionary
        Object[] loadedMaterials = Resources.LoadAll("Materials/BallMaterials");

        foreach (Material material in loadedMaterials)
        {
            string name = material.name.Remove(0, 13).ToLowerInvariant();

            materialDictionary.Add(material.name, material);
        }
    }

    public void AddBall(string name, BallMaterial ballMaterial)
    {
        if (!ballDictionary.ContainsKey(name))
        {
            //Create ball
            GameObject ball = Instantiate(ballPrefab);

            ball.transform.parent = parent;
            ball.name = name;

            //Add to dictionary
            ball.transform.position = spawnPointTransform.Find("SP" + ballDictionary.Count % spawnPointAmount).position;
            ballDictionary.Add(name, ball);

            MeshRenderer meshRenderer = ball.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();

            //Set material
            switch (ballMaterial)
            {
                case BallMaterial.RANDOM:
                    BallMaterial rand = (BallMaterial)Random.Range(1f, 6f);
                    if (materialDictionary.ContainsKey("BallMaterial" + rand.ToString()))
                    {
                        meshRenderer.material = materialDictionary["BallMaterial" + rand.ToString()];
                    }
                    else
                    {
                        Debug.LogError("Error! Attempted to load a random material which could not be found!");
                    }
                    break;

                default:
                    if (materialDictionary.ContainsKey("BallMaterial" + ballMaterial.ToString()))
                    {
                        meshRenderer.material = materialDictionary["BallMaterial" + ballMaterial.ToString()];
                    }
                    else
                    {
                        Debug.LogError("Error! Attempted to load the material " + ballMaterial.ToString() + " which could not be found!");
                    }
                    break;
            }
        }
    }

    public void AddBall(string name)
    {
        AddBall(name, BallMaterial.RANDOM);
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
        if (Input.GetButtonDown("FIRE1"))
        {
            //TEMP rng ball spawn
            AddBall(ballDictionary.Count.ToString());
        }
    }
}
