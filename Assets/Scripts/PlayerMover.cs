using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public SpriteRenderer CharacterSpriteRenderer;
    public float movespeed = 6f;
    private Vector2 InputVector = Vector2.zero;

    [SerializeField] Rewindable rewinder = default;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 dir)
    {
        InputVector = dir;
    }
    void Update()
    {

        try {
            if (rewinder.rewinding) return;
        } catch {}
        rigidbody2d.velocity = InputVector * movespeed;
        if (rigidbody2d.velocity.x < 0) CharacterSpriteRenderer.flipX = true;
        else CharacterSpriteRenderer.flipX = false;

    }
}
