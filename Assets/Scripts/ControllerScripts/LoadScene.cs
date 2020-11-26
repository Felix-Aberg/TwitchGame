using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update

    public bool loadInstantly;
    public int instantLoadIndex;

    private void Start()
    {
        if (loadInstantly)
        {
            SceneManager.LoadScene(instantLoadIndex);
        }
    }


    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
