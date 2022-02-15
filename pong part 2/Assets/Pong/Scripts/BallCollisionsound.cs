using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionsound : MonoBehaviour
{
    
    public AudioClip pongSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = pongSound;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        var speed = rigidbody.velocity.magnitude;
        audioSource.pitch = (float) (1.0+ (speed*0.05));
        audioSource.Play();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUP"))
        {
            other.gameObject.SetActive(false);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = rigidbody.velocity * 2;
        }
        if (other.gameObject.CompareTag("PowerDown"))
        {
            other.gameObject.SetActive(false);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = rigidbody.velocity * (float) 0.5;
        }
    }
}
