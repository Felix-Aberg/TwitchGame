using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    [SerializeField] List<string> randomNames;

    [Tooltip("Number for ball names when random names run out")]
    int listRepetitions = 1;

    // Start is called before the first frame update
    void Start()
    {
        randomNames = System.IO.File.ReadAllLines("Assets/Resources/names.txt").ToList();
    }

    //TODO: check if name is already in use before returning value
    public string GetRandomName()
    {
        if (randomNames.Count == 0)
        {
            randomNames = System.IO.File.ReadAllLines("Assets/Resources/names.txt").ToList();
            listRepetitions++;
            for (int i = 0; i < randomNames.Count; i++)
            {
                randomNames[i] += " " + listRepetitions.ToString();
            }
        }
        int rand = Random.Range(0, randomNames.Count - 1);
        string returnValue = randomNames[rand];
        randomNames.RemoveAt(rand);
        return returnValue;
    }
}
