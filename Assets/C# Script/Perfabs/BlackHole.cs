using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private bool isCoroutineExecuting;

    public bool BlackHoleIsOn { get; private set; }

    private void Start()
    {
        BlackHoleIsOn = false;
        isCoroutineExecuting = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Donky>();
        if (player != null && !BlackHoleIsOn)
        {
            BlackHoleIsOn = true;
            CameraFollow.instance.isActive = false;

            Donky.instance.onBlcakHole(GetComponent<CircleCollider2D>().bounds.center);
            GetComponent<AudioSource>().Play();

            App.instance.SaveData();
            StartCoroutine(ExecuteAfterTime(0.5f, () =>
            {
                Can_GameOver.instance.Show();
            }));
        }
    }


    public IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }

}