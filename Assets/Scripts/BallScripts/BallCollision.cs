﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    Rigidbody rigidbody;
    public float pushForce;
    public ParticleSystem sparks;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            //Placeholder collision, push each other away by
            float velocity = rigidbody.velocity.magnitude;
            Vector3 push = transform.position - collision.transform.position;
            push = push.normalized * pushForce;
            float RNGmultiplier = Random.Range(0.5f, 1.4f);
            collision.rigidbody.AddForce(-push * velocity * RNGmultiplier);

            sparks.Play();

        }
    }
    
}
