using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    TournamentController tournament;
    MatchController match;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for
        //a scene change as soon as this script is disabled. Remember to always 
        //have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    

    private void Update()
    {
        CheckControlInputs();
    }

    void CheckControlInputs()
    {
        if (Input.GetButton("CANCEL"))
        {
            Exit();
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);

        if (scene.name == "ModeSelect")
        {
            LoadModeSelect();
        }
        else if (scene.name == "LevelSelect")
        {
            LoadLevelSelect();
        }
        else if (scene.name.StartsWith("Level "))
        {
            LoadedLevel();
        }
    }

    void LoadModeSelect()
    {
        tournament.LoadModeSelect();
    }

    void LoadLevelSelect()
    {
        //
        tournament.LoadLevelSelect();
    }

    void LoadedLevel()
    {
        tournament.LoadedLevel();

        //Spawn match manager
        match = Instantiate(Resources.Load("Prefabs/CorePrefabs/MatchController") as GameObject).GetComponent<MatchController>();
    }

    void Exit()
    {
        //call the abstract class

        //Load scene 0 if level select can't be found
        if (SceneManager.GetSceneByName("LevelSelectScene") == null)
        {
            Debug.LogError("ERROR! Couldn't find LevelSelectScene!");
            SceneManager.LoadScene(0);
        }

        SceneManager.LoadScene("ModeSelect");


    }

    public void startTournament(int matches)
    {
        if (matches == -1)
        {
            matches = int.Parse(GameObject.FindGameObjectWithTag("AmountOfGamesDisplay").GetComponent<TextMeshProUGUI>().text);
        }

        SceneManager.LoadScene("LevelSelect");

        //Spawn tournament manager
        tournament = Instantiate(Resources.Load("Prefabs/CorePrefabs/TournamentController") as GameObject).GetComponent<TournamentController>();
        tournament.totalMatches = matches;
    }
}
