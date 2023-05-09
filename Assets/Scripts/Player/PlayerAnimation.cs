using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private string currentAnimation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ChangeAnimation(string p_newAnimation)
    {
        if(currentAnimation != p_newAnimation)
        {
            animator.Play(p_newAnimation);
            currentAnimation = p_newAnimation;
        }
    }

    public void AnimationIdle()
    {
        ChangeAnimation("Idle");
    }

    public void AnimationRun()
    {
        ChangeAnimation("Run");
    }

    public void AnimationJump()
    {
        ChangeAnimation("Jump");
    }

    public void AnimationDead()
    {
        ChangeAnimation("Dead");
    }
}
