using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlatfrom : MonoBehaviour
{

    private bool isCoroutineExecuting;

    public Color targetColor;


    private float t;

    private void OnEnable()
    {
        t = UnityEngine.Random.Range(1f, 5.5f);

        targetColor= new Color(0.9811321f, 0.4557808f, 0.0786757f, 1);

        StartCoroutine(LerpFunction(targetColor, t));


        StartCoroutine(ExecuteAfterTime(t, () =>
        {
            App.Instance.PlayExplosion();
            Destroy(gameObject);
        }));


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

    IEnumerator LerpFunction(Color endValue, float duration)
    {
        float time = 0;
        Color startValue = GetComponent<SpriteRenderer>().color;
        while (time < duration)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(startValue, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = endValue;
    }


}
