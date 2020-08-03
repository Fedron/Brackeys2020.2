using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float movespeed = 6f;
    private Vector2 InputVector = Vector2.zero;

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
        //rigidbody2d.MovePosition(rigidbody2d.position + InputVector * movespeed * Time.deltaTime);
        //Vector2 direction = (InputVector).normalized;
        //Vector2 force = direction * movespeed * Time.deltaTime;

        //rigidbody2d.AddForce(force);
        rigidbody2d.velocity = InputVector * movespeed;
    }
}
