using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMonester : MonoBehaviour
{
    public AudioSource slaping;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<Donky>();
        if (player != null)
        {
            if (collision.relativeVelocity.y > 0)
            {
                slaping.Play();
                player.Drop();
            }
        }
    }

    internal void Drop()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
}
