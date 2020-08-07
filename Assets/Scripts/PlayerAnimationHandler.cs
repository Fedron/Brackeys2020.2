using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Animator animator;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb2d.velocity.magnitude > 0.1) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
    }
    public void DieAnimation()
    {
        animator.SetBool("isDead", true);
    }

    public void ReviveAnimation()
    {
        animator.SetBool("isDead", false);
    }
}
