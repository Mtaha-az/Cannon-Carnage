using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;
    private float animationTime;

    void Start()
    {
        // Get the Animator component attached to the explosion prefab
        animator = GetComponent<Animator>();

        // Calculate the total length of the animation
        animationTime = animator.GetCurrentAnimatorStateInfo(0).length;

        // Destroy the explosion object after the animation is complete
        Destroy(gameObject, animationTime);
    }
}
