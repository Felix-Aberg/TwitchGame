using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This ability is the bomb spawned by the ball ability 
/// </summary>
public class BallBomb : MonoBehaviour
{

    BallCollision ballCollision;


    Camera cam;
    Vector3 worldOffset;
    Vector3 canvasOffset;

    float explosionDamage = 75f;
    float explosionRange = 1f;
    public float maxCooldown = 0.1f;
    
    public Text timerText;
    public float explosionTimer = 10f;
    public string originPlayer;
    public float cooldown = 0f;

    public void SetOrigin(string origin)
    {
        PassValues(explosionTimer, originPlayer, timerText);
    }

    /// <summary>
    /// Pass the values from the previous bomb script
    /// </summary>
    public void PassValues(float timer, string origin, Text text)
    {
        explosionTimer = timer;
        originPlayer = origin;
        timerText = text;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this might break everything
        //this might break everything
        //this might break everything
        //this might break everything
        //this might break everything
        //this might break everything
        if (timerText == null)
        {
            Destroy(this);
        }

        // Reduce timers
        explosionTimer -= Time.fixedDeltaTime;
        cooldown -= Time.fixedDeltaTime;

        // Update visual
        string display = (explosionTimer + 1f).ToString();
        display = display.Substring(0, display.IndexOf("."));
        timerText.text = display;

        if (timerText.transform.position.z < 0)
        {
            timerText.gameObject.SetActive(false);
        }
        else
        {
            timerText.gameObject.SetActive(true);
        }

        if (explosionTimer < 0f)
        {
            // Remove timer, explode
            Explode();
            Debug.Log("bomb exploded!");
            Destroy(this);
        }
    }

    private void Update()
    {
        timerText.transform.parent.position = cam.WorldToScreenPoint(transform.position + worldOffset) + canvasOffset;
    }

    void Explode()
    {
        GetComponent<BallCollision>().lastHitByName = originPlayer;
        Debug.Log("origin: " + originPlayer);

        GetComponent<BallDurability>().RPM -= explosionDamage;

        //destroy bomb command component if it exists
        if (gameObject.TryGetComponent<BallCmdBomb>(out var component))
        {
            Debug.Log("Destroyed bomb command!");
            component.SelfDestruct();
            Destroy(this);
        }

        Instantiate(
            Resources.Load("ParticleEffects/Command Particles/Bomb Explosion"));
        // Suicide
        //GetComponent<Ball>().SelfDestruct();
    }

    void Start()
    {
        ballCollision = GetComponent<BallCollision>();
        ballCollision.hasBomb = true;
        cam = FindObjectOfType<Camera>();

        worldOffset = new Vector3(0f, 1f, 0f);
        canvasOffset = new Vector3(0f, 50f, 0f);
    }

    void OnDestroy()
    {
        if (explosionTimer < 0f)
        {
            Destroy(timerText.transform.parent.gameObject);
        }
        ballCollision.hasBomb = false;
    }
}
