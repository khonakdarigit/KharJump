using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Rocket = collision.GetComponent<Roket>();
        var Donkey = collision.GetComponent<Donky>();

        // if Reqular_Platfrom
        if (collision.gameObject.name.Contains(PlatformGenerate.PlatformType.Reqular_Platfrom.ToString()))
        {
            PlatformGenerate.instance.DedPlatform(collision.gameObject);
        }
        else if (Donkey != null)
        {

        }
        else// if (Rocket == null && Donky.instance.staus != Donky.DonkeyStaus.InRocketFly)
        {
            UnityEngine.Object.Destroy(collision.gameObject);
        }
    }
}
