using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDounky : MonoBehaviour
{
    public float speed;
    public float degree;
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, speed);
        if (gameObject.transform.rotation.z > degree ||
            gameObject.transform.rotation.z < -degree)
        {
            speed *= -1;
        }
    }
}
