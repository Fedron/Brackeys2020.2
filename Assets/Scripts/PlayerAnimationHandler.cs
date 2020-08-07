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
    public void WalkAnimation()
    {
        
    }
    public void DieAnimation()
    {
        //todo remove debug log
        Debug.Log("Die animation is happening...");
        //todo implement die animation
    }

    public void ReviveAnimation()
    {
        //todo remove debug log
        Debug.Log("Revive animation is happening");
        //todo implement revive animation
    }
}
