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
        Destroy(gameObject, existanceTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it touches the player
        if (collision.transform.CompareTag(targetTag))
        {
            Destroy(gameObject);
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
