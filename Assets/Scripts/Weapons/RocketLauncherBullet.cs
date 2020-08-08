using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherBullet : MonoBehaviour
{
    [SerializeField]
    private float existanceTime = 1f;
    [SerializeField]
    private float bulletSpeed = 15f;
    [SerializeField]
    private string targetTag = default;
    private Rigidbody2D rb2d;
    public GameObject afterBulletsPref, blowupVFX;
    public Transform[] TransformBulletsSpawners;
    public float Damage { get; set; }
    public float LittleBulletsDamage;
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
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<Rewindable>().rewinding) return;
            }
            var health = collision.transform.GetComponent<IHaveHealth>();
            if (health != null)
            {
                Instantiate(blowupVFX, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySound2D("RocketExplosion1");
                health.GetDamage(Damage);
                Destroy(gameObject);
            }
        }
        else if (collision.transform.CompareTag("Wall"))
        {
            if (currentNumOfRicochets < maxNumofRicochets) currentNumOfRicochets++;
            else Destroy(gameObject);
        }
    }

    private void OnDestroy() => BlowWithShreds();
    void BlowWithShreds()
    {
        Instantiate(blowupVFX, transform.position, Quaternion.identity);
        //todo spawn a particle effect for fire muzzle
        //Instantiate(MuzzleParticlePref, shootingFromTransform[0].position, shootingFromTransform[0].rotation);
        foreach (Transform spawnPoint in TransformBulletsSpawners)
        {
            var bullet = Instantiate(afterBulletsPref, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Bullet>().Damage = LittleBulletsDamage;
        }
        AudioManager.Instance.PlaySound2D("RocketExplosion1");
    }

}
