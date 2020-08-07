using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimation : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rb2d.velocity.magnitude > 0.1) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
    }
}
