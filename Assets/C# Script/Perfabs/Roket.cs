using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class Roket : MonoBehaviour
{
    public GameObject Fier1;
    public GameObject Fier2;
    public bool StartedFier { get; set; }
    public bool DanceFier { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        DanceFier = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if (StartedFier)
        {
            DanceFier = !DanceFier;
            Fier1.gameObject.SetActive(!DanceFier);
            Fier2.gameObject.SetActive(DanceFier);
        }

    }

    internal void StartFier()
    {
        StartedFier = true;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<AudioSource>().Play();
    }
    internal void StopFier()
    {
        StartedFier = false;
        Fier1.gameObject.SetActive(false);
        Fier2.gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.85f;
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = -20f;
    }

}
