using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerCreator : MonoBehaviour
{
    public GameObject GameController;

    private void Start()
    {
        //If there are no GameControllers;
        if (GameObject.FindGameObjectsWithTag("GameController").Length == 0)
        {
            //Create a gameController
            Instantiate(GameController);
        }
    }
}
