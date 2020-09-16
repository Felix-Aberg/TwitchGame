using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    BallManager ballManager;

    // Start is called before the first frame update
    void Start()
    {
        ballManager = gameObject.AddComponent<BallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
