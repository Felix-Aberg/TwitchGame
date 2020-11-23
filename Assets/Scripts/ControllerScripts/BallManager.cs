using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(NameGenerator))]
public class BallManager : MonoBehaviour
{
    //I am really not sure if I should exclusively be using a dictionary
    public Dictionary<string, GameObject> ballDictionary;
    public Dictionary<string, Material> materialDictionary;

    NameGenerator nameGenerator;
    public BallConfig ballConfig;

    [HideInInspector] public Transform parent;

    GameObject ballPrefab;
    GameObject botPrefab;
    int spawnPointAmount;
    int spawnPointRepetition = -1;
    Transform spawnPointTransform;
    [HideInInspector] public List<Transform> unusedSpawnpoints;


    private void Start()
    {
        ballDictionary = new Dictionary<string, GameObject>();
        materialDictionary = new Dictionary<string, Material>();
        nameGenerator = GetComponent<NameGenerator>();

        parent = Instantiate(new GameObject()).transform;
        parent.name = "Balls";
        ballPrefab = Resources.Load("Prefabs/Spinnytops/Ball") as GameObject;
        botPrefab = Resources.Load("Prefabs/Spinnytops/BotBall") as GameObject;

        if(ballPrefab == null)
        {
            Debug.LogError("Couldn't load player ball in BallManager!");
        }

        if(botPrefab == null)
        {
            Debug.LogError("Couldn't load player ball in BallManager!");
        }

        spawnPointTransform = transform.Find("SpawnPoints");
        spawnPointAmount = spawnPointTransform.childCount;

        //Add materials to dictionary
        Object[] loadedMaterials = Resources.LoadAll("Materials/BallMaterials");

        foreach (Material material in loadedMaterials)
        {
            //TODO: what's this??
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
            Transform sp = spawnPointTransform.Find("SP" + i);
            if (sp.gameObject.activeSelf)
                unusedSpawnpoints.Add(sp);
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

            ball.transform.GetChild(0).gameObject.GetComponent<Ball>().gameController = gameObject;
            ball.transform.GetChild(0).gameObject.GetComponent<BallCollision>().ballConfig = ballConfig;
            ball.transform.GetChild(0).gameObject.GetComponent<BallCollision>().InitializeConfig();

            //Add to dictionary
            if (unusedSpawnpoints.Count == 0)
            {
                GenerateSpawnPoints();
                Debug.Log("Ran out of spawnpoints, reused them");
            }

            ball.transform.position = GetSpawnPoint();
            ballDictionary.Add(name, ball);
            GetComponent<PlayerCount>().AddPlayer();
            if (GetComponent<PlayerCount>() == null)
            {
                Debug.LogError("ERROR! PlayerCount's text is not set in GameController. Did you apply it in this scene?");
            }

            MeshRenderer meshRenderer = ball.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>();

            //Set material
            switch (ballMaterial)
            {
                case BallMaterial.RANDOM:
                    BallMaterial rand = (BallMaterial) (ballDictionary.Count % materialDictionary.Count) + 1;
                    if (materialDictionary.ContainsKey("BallMaterial" + rand.ToString()))
                    {
                        meshRenderer.material = materialDictionary["BallMaterial" + rand.ToString()];
                        Debug.Log(meshRenderer.material);
                    }
                    else
                    {
                        Debug.LogError("Error! Attempted to load a random material (" + rand + ")which could not be found!");
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

            if (ballMaterial == BallMaterial.GOLD)
            {
                MeshRenderer mr = ball.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
            }
        }
        else
        {
            Debug.LogWarning("Error! Attempted to create ball using a name that already exists");
        }
    }

    public void AddBot()
    {
        AddBot(false);
    }

    public void AddBot(bool useRandomColor)
    {
        string name = nameGenerator.GetRandomName();
        if (!ballDictionary.ContainsKey(name))
        {
            //Create ball
            GameObject ball = Instantiate(botPrefab);

            ball.transform.parent = parent;
            ball.name = name;
            ball.transform.GetChild(0).GetComponent<Ball>().gameController = gameObject;
            //not for bots
            //ball.transform.GetChild(0).GetComponent<BallCollision>().ballConfig = ballConfig;
            ball.transform.GetChild(0).GetComponent<BallCollision>().InitializeConfig();

            //Add to dictionary
            if (unusedSpawnpoints.Count == 0)
            {
                GenerateSpawnPoints();
                Debug.Log("Ran out of spawnpoints, reused them");
            }

            ball.transform.position = GetSpawnPoint();
            ballDictionary.Add(name, ball);
            GetComponent<PlayerCount>().AddPlayer();
            if (GetComponent<PlayerCount>() == null)
            {
                Debug.LogError("ERROR! PlayerCount's text is not set in GameController. Did you apply it in this scene?");
            }

            if (useRandomColor)
            {
                MeshRenderer meshRenderer = ball.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
                Debug.Log(ball.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).name);
                BallMaterial rand = (BallMaterial)(ballDictionary.Count % materialDictionary.Count) + 1;
                if (materialDictionary.ContainsKey("BallMaterial" + rand.ToString()))
                {
                    meshRenderer.material = materialDictionary["BallMaterial" + rand.ToString()];
                }
                else
                {
                    Debug.LogError("Error! Attempted to load a random material (" + rand + ")which could not be found!");
                }
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
            AddBot();
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetButton("SPAWNBALL"))
        {
            AddBot();
        }
    }
}
