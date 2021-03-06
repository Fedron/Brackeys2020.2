﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float existanceTime = 1f;
    [SerializeField]
    private float bulletSpeed = 15f;
    [SerializeField]
    private string targetTag = default;
    private Rigidbody2D rb2d;
    public float Damage { get; set; }
    public int maxNumofRicochets, currentNumOfRicochets = 0;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2d.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Vector2 direction = rb2d.velocity;
        float rotateAmount = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateAmount);
        Destroy(gameObject, existanceTime);
    }

    private void FixedUpdate()
    {
        Vector2 direction = rb2d.velocity;
        float rotateAmount = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotateAmount);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it touches the player
        if (collision.transform.CompareTag(targetTag))
        {
            Destroy(gameObject);
            if (collision.gameObject.CompareTag("Player")) {
                if (collision.gameObject.GetComponent<Rewindable>().rewinding) return;
            } 
            var health = collision.transform.GetComponent<IHaveHealth>();
            if (health != null)
                health.GetDamage(Damage);
        }
        else if (collision.transform.CompareTag("Wall"))
        {
            if (currentNumOfRicochets < maxNumofRicochets) currentNumOfRicochets++;
            else Destroy(gameObject);
        }
    }
}
