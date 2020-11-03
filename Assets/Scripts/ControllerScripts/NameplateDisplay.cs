using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class NameplateDisplay : MonoBehaviour
{
    GameObject nameplateCanvas;
    public GameObject nameplatePrefab;
    [HideInInspector] public GameObject nameplate;
    private Transform nameplateTransform;
    Text text;

    private Camera cam;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        nameplateCanvas = GameObject.FindGameObjectWithTag("NameplateCanvas");
        nameplate = Instantiate(nameplatePrefab, nameplateCanvas.transform);
        nameplate.name = name;

        text = nameplate.GetComponent<Text>();
        text.text = name;

        nameplateTransform = nameplate.transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        nameplateTransform.position = cam.WorldToScreenPoint(transform.position + offset);
        if (nameplateTransform.position.z < 0)
        {
            nameplate.SetActive(false);
        }
        else
        {
            nameplate.SetActive(true);
        }
    }
}
