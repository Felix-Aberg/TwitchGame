using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(NameGenerator))]
public class BallManager : MonoBehaviour
{
    //I am really not sure if I should exclusively be using a dictionary
    public Dictionary<string, GameObject> ballDictionary;
    public Dictionary<string, Material> materialDictionary;

    NameGenerator nameGenerator;

    GameObject ballPrefab;
    Transform parent;
    public int spawnPointAmount;
    int spawnPointRepetition = -1;
    Transform spawnPointTransform;
    public List<Transform> unusedSpawnpoints;


    private void Start()
    {
        ballDictionary = new Dictionary<string, GameObject>();
        materialDictionary = new Dictionary<string, Material>();
        nameGenerator = GetComponent<NameGenerator>();

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

        GenerateSpawnPoints();
    }

    Vector3 GetSpawnPoint()
    {
        int rand = Random.Range(0, unusedSpawnpoints.Count - 1);
        Vector3 returnValue = unusedSpawnpoints[rand].position;
        unusedSpawnpoints.RemoveAt(rand);
        returnValue.y += spawnPointRepetition * 3f; //dummy number but its ok
        return returnValue;
    }

    void GenerateSpawnPoints()
    {
        for (int i = 0; i < spawnPointTransform.childCount; i++)
        {
            unusedSpawnpoints.Add(spawnPointTransform.Find("SP" + i));
        }
        spawnPointRepetition += 1;
        return;
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
            if (unusedSpawnpoints.Count == 0)
            {
                GenerateSpawnPoints();
                Debug.Log("Ran out of spawnpoints, reused them");
            }

            ball.transform.position = GetSpawnPoint();
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
        else
        {
            Debug.LogWarning("Error! Attempted to create ball using a name that already exists");
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
        if (Input.GetButtonDown("SPAWNBALL"))
        {
            //TEMP rng ball spawn
            AddBall(nameGenerator.GetRandomName());
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetButton("SPAWNBALL"))
        {
            //TEMP rng ball spawn
            AddBall(nameGenerator.GetRandomName());
        }
    }
}
