using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightJumpHidePlatform : MonoBehaviour
{
    float speed = 60f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -(Vector3.right * speed * 2 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (gameObject.transform.position.x >= PlatformGenerate.instance.HalfCameraSize)
        {
            GetComponent<Rigidbody2D>().velocity = -(Vector3.right * speed * 2 * Time.deltaTime);
        }
        else if (gameObject.transform.position.x <= (-1 * PlatformGenerate.instance.HalfCameraSize))
        {
            GetComponent<Rigidbody2D>().velocity = (Vector3.right * speed * 2 * Time.deltaTime);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
