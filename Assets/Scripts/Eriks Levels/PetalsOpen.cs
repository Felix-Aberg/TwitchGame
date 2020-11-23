using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalsOpen : MonoBehaviour
{
    public Quaternion startQ;
    public Quaternion targetQ;

    public float smooth = 5f;

    public bool ping;

    public float speed = 15f;

    public float minTimer;
    public float maxTimer;
    [SerializeField]

    float timerStart;
    float timer;

    float deltaTime;
    public float offset;
    

    // Start is called before the first frame update
    void Start()
    {
        timerStart = Random.Range(minTimer, maxTimer);
        timer = timerStart;

        startQ = transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveSine();
    }

    void MoveTowards(bool forwards)
    {
        Quaternion a;
        Quaternion b;
        float t;

        if (forwards)
        {
            a = startQ;
            b = targetQ;
        }
        else
        {
            a = targetQ;
            b = startQ;
        }


        transform.localRotation = Quaternion.Slerp(a, b, speed * Time.fixedDeltaTime);
    }

    void MoveSine()
    {
        Quaternion a;
        Quaternion b;
        float t;

        a = startQ;
        b = targetQ;

        deltaTime += Time.fixedDeltaTime;

        t = (Mathf.Sin((deltaTime * speed) + offset) + 1) / 2;

        transform.localRotation = Quaternion.Slerp(a, b, t);
    }
}
