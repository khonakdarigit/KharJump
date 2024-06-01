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

    private Vector3 _camera_TargetPosition;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothSpeed = 0.220f;

    private void Awake()
    {
        instance = this;


        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            // Change position with Donkey
            if (doodlePos.position.y > transform.position.y)
            {
                _camera_TargetPosition = new Vector3(transform.position.x, doodlePos.position.y, transform.position.z);
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
                    _camera_TargetPosition = new Vector3(transform.position.x, doodlePos.position.y + additionY, transform.position.z);
                }
                else
                {
                    additionY = transform.position.y - doodlePos.position.y;
                }
            }

            if (_camera_TargetPosition != null)
                transform.position = Vector3.SmoothDamp(transform.position, _camera_TargetPosition, ref velocity, smoothSpeed);
        }

       
    }
    private void FixedUpdate()
    {
      
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
