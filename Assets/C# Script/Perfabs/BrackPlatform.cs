using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrackPlatform : MonoBehaviour
{
    public Rigidbody2D
        P_L,
        P_R;
    private bool rotate;

    public void Brack()
    {
        BrackSound();

        P_L.bodyType = RigidbodyType2D.Dynamic;
        P_R.bodyType = RigidbodyType2D.Dynamic;
        rotate = true;

        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public void BrackSound()
    {
        GetComponent<AudioSource>().Play();
    }

    private void FixedUpdate()
    {
        if (rotate)
        {
            P_L.AddTorque(-10f,ForceMode2D.Force);
            P_R.AddTorque(10f, ForceMode2D.Force);

        }
    }

}
