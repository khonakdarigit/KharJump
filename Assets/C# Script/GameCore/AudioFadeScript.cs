using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AudioFadeScript : MonoBehaviour
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTimeDuration, float toVolume)
    {


        //float startVolume = audioSource.volume;

        while (audioSource.volume > toVolume)
        {
            audioSource.volume -= 0.2f * Time.deltaTime / FadeTimeDuration;

            yield return null;
        }

        if (toVolume == 0f)
            audioSource.Stop();

        audioSource.volume = toVolume;

    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTimeDuration, float toVolume)
    {
        float stepVolume = 0.2f;

        if (!audioSource.isPlaying)
        {
            audioSource.volume = 0f;
            audioSource.Play();
        }
        while (audioSource.volume < toVolume)
        {
            audioSource.volume += stepVolume * Time.deltaTime / FadeTimeDuration;

            yield return null;
        }

        audioSource.volume = toVolume;
    }
}
