using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetTrigger(CharAnimsTag animTag)
    {
        animator.SetTrigger(animTag.ToString());
    }
}

public enum CharAnimsTag
{
    win,
    lose
}