using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysRotateTowardsTarget : MonoBehaviour
{
    private Rigidbody2D target;
    private SpriteRenderer spriteRend;
    private void Awake()
    {
        target = FindObjectOfType<PlayerInputHandler>().GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        Vector2 lookDir = target.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270) { spriteRend.flipY = true; }
        else spriteRend.flipY = false;
    }
}
