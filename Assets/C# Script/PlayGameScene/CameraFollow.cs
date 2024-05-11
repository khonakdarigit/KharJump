using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform doodlePos;
    public float camaraSpeedFollowDonkeyAfterGameOver;

    private float additionY;
    private bool followDonkeyOnGameOver = false;
    public bool isActive;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        isActive = true;
    }
    private void FixedUpdate()
    {
        if (isActive)
        {
            // Change position with Donkey
            if (doodlePos.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, doodlePos.position.y, transform.position.z);
            }
            else
            {
                if (followDonkeyOnGameOver)
                {
                    if (additionY > 0)
                    {
                        additionY -= camaraSpeedFollowDonkeyAfterGameOver;
                        additionY = additionY < 0 ? 0 : additionY;
                    }
                    transform.position = new Vector3(transform.position.x, doodlePos.position.y + additionY, transform.position.z);
                }
                else
                {
                    additionY = transform.position.y - doodlePos.position.y;
                }
            }
        }
    }

    internal void FollowDonkeyOnGameOver()
    {
        followDonkeyOnGameOver = true;
    }

    internal void UnFollowDonkeyOnGameOver()
    {
        followDonkeyOnGameOver = false;
    }
    // Update is called once per frame



}
