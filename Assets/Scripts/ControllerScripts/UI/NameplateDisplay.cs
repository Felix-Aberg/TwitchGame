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

    public Color maxHealthColor;
    public Color minHealthColor;

    // Start is called before the first frame update
    void Start()
    {
        nameplateCanvas = GameObject.FindGameObjectWithTag("NameplateCanvas");
        nameplate = Instantiate(nameplatePrefab, nameplateCanvas.transform);
        nameplate.name = transform.parent.name;

        text = nameplate.GetComponent<Text>();
        text.text = transform.parent.name;

        nameplateTransform = nameplate.transform;
        cam = Camera.main;
    }

    /// <summary>
    /// Color the name according to the player's durability
    /// </summary>
    /// <param name="durability"> From 0-100 </param>
    public void ColorName(float durability)
    {
        if (durability < 50f)
        {
            text.color = Color.Lerp(minHealthColor, maxHealthColor, durability * 0.01f);
        }
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
