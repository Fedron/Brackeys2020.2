using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float movespeed = 6f;
    private Vector2 InputVector = Vector2.zero, mouseVector = Vector2.zero;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 dir)
    {
        InputVector = dir;
    }

    public void SetMouseDirection(Vector2 dir)
    {
        mouseVector = dir;
    }
    void Update()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + InputVector * movespeed * Time.deltaTime);

        Vector2 lookDir = mouseVector - rigidbody2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rigidbody2d.rotation = angle;
    }
}
