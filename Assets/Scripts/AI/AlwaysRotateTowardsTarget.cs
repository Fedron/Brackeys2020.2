using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysRotateTowardsTarget : MonoBehaviour
{
    private Rigidbody2D target;

    private void Awake()
    {
        target = FindObjectOfType<PlayerInputHandler>().GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 lookDir = target.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
