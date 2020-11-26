using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public enum ScoreEvent
{
    PLACE1, PLACE2, PLACE3, PLACE4, PLACE5, KILL, BOUNTYKILL, FIRSTBLOOD, REVENGEKILL, BOTKILL, 
}

[System.Serializable]
public class TempScoreDisplay : MonoBehaviour
{
    public Text text;

    public string bountyName;
    List<KeyValuePair<string, int>> list;
    Dictionary<string, int> dictionary;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        list = new List<KeyValuePair<string, int>>();
        dictionary = new Dictionary<string, int>();

        /*
        //Example of adding score
        AddScore("First", ScoreEvent.PLACE1);
        AddScore("Seconds", ScoreEvent.REVENGEKILL);
        AddScore("Third", ScoreEvent.KILL);
        AddScore("Fourth", ScoreEvent.BOTKILL);
        AddScore("Fifth", ScoreEvent.BOUNTYKILL);
        AddScore("Sixth", 5);
        */
    }

    private void Update()
    {
                                                          // haha take the L yo
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L))
        {
            dictionary.Clear();
            list.Clear();
        }
    }

    /// <summary>
    /// Add score to user
    /// </summary>
    /// <param name="username">Case insensitive, is used as lower case</param>
    public void AddScore(string username, int score)
    {
        if(dictionary.ContainsKey(username))
        {
            dictionary[username] += score;
        }
        else
        {
            dictionary.Add(username, score);
        }
        SortList();
    }

    public void AddScore(string username, ScoreEvent scoreEvent)
    {
        int add = 0;
        switch(scoreEvent)
        {
            case ScoreEvent.PLACE1:
                add = 10;
                break;
            case ScoreEvent.PLACE2:
                add = 5;
                break;
            case ScoreEvent.PLACE3:
                add = 3;
                break;
            case ScoreEvent.PLACE4:
                add = 2;
                break;
            case ScoreEvent.PLACE5:
                add = 1;
                break;
            case ScoreEvent.KILL:
                add = 1;
                break;
            case ScoreEvent.BOUNTYKILL:
                add = 5;
                break;
            case ScoreEvent.FIRSTBLOOD:
                add = 2;
                break;
            case ScoreEvent.REVENGEKILL:
                add = 2;
                break;
            case ScoreEvent.BOTKILL:
                add = 0;
                break;
            default:
                break;
        }
        AddScore(username, add);
    }

    /// <summary>
    /// Sorts list
    /// </summary>
    void SortList()
    {

        list = dictionary.ToList<KeyValuePair<string, int>>();

        Debug.Log("<b>Pre Sort:</b>");
        foreach (var pair in list)
        {
            Debug.Log(pair);
        }

        //sort
        list.Sort(CompareValues);
        list.Reverse();
        Debug.Log("<b>Post Sort:</b>");
        foreach (var pair in list)
        {
            Debug.Log(pair);
        }

        UpdateUI();
    }

    /// <summary>
    /// Helper function for the sort
    /// </summary>
    static int CompareKeys(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
    {
        return a.Key.CompareTo(b.Key);
    }

    /// <summary>
    /// Helper function for the sort
    /// </summary>
    static int CompareValues(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
    {
        return a.Value.CompareTo(b.Value);
    }

    /// <summary>
    /// Displays the sorted list on the text canvas
    /// </summary>
    void UpdateUI()
    {
        //Reset text
        text.text = "";

        //Insert all elements
        foreach (var pair in list)
        {
            text.text += pair.Key + ": " + pair.Value + "\n";
        }

        //Remove the last newline
        text.text = text.text.Remove(text.text.Length - 1);
    }

    List<KeyValuePair<string, int>> GetList()
    {
        return list;
    }
}
