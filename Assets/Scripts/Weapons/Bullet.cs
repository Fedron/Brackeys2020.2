using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float existanceTime = 1f;
    [SerializeField]
    private float bulletSpeed = 15f;
    private Rigidbody2D rb2d;
    public float damage;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb2d.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, existanceTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<IHaveHealth>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
