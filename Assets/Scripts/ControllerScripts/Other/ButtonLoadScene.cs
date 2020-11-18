using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLoadScene : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);

        //if level.name startswith: call gamecontroller?
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartTournamemt(int matches)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().startTournament(matches);
    }
}
