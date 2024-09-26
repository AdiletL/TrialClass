using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] public AnimationClip runClip;
    [SerializeField] public AnimationClip jumpClip;
    [SerializeField] public AnimationClip runJumpClip;

    private Animator animator;
    private AnimationClip currentClip;
    private List<AnimationClip> clips;

    private float currentSpeed;

    private bool isGrounded;

    private const string transitionName = "speed";

    private void Awake()
    {
        animator = GetComponent<Animator>();

        clips = new List<AnimationClip>(animator.runtimeAnimatorController.animationClips.Length);
        foreach (var item in animator.runtimeAnimatorController.animationClips)
            clips.Add(item);
    }
    private float GetAnimationDuration(AnimationClip clip)
    {
        return clips.Find(item => item == clip).length;
    }

    public void UpdateIdle()
    {
        if(isGrounded) SwitchAnimation(idleClip);
    }
    public void UpdateRun(float movementSpeed)
    {
        if (isGrounded) SwitchAnimation(runClip, division: 2, duration: movementSpeed);
    }
    public void UpdateJump(bool isGrounded)
    {
        this.isGrounded = isGrounded;
        if(!isGrounded) SwitchAnimation(jumpClip);
    }

    public void SwitchAnimation(AnimationClip clip, float division = 1, float duration = 1, float transitionDuration = .1f)
    {
        if (clip == null || clip == currentClip) return;
        SetSpeedAnimatoin(ref clip, division, duration);
        animator.CrossFadeInFixedTime(clip.name, transitionDuration, 0);
        currentClip = clip;
    }

    private void SetSpeedAnimatoin(ref AnimationClip clip, float division = 1, float duration = 1)
    {
        float speed = (GetAnimationDuration(clip) / division) * duration;
        if (currentSpeed == speed) return;
        animator.SetFloat(transitionName, speed);
        currentSpeed = speed;
    }
}
