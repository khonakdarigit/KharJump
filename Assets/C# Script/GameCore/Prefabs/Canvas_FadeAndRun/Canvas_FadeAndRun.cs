using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_FadeAndRun : Singleton<Canvas_FadeAndRun>
{
    [SerializeField] GameObject back;
    [SerializeField] Animator animator;
    private Action _action;

    public void FadIn()
    {
        back.SetActive(true);
        animator.SetTrigger("FadeIn");
    }

    public void FadeOutAndRun(Action action)
    {
        _action = action;
        animator.SetTrigger("FadeOut");
    }
    public void EndFadeOut()
    {
        _action?.Invoke();
        _action = null;
    }
}
