using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooting : MonoBehaviour, IShootingWeapon
{
    public Transform shootingFromTransform;
    public float maxRayCastRange;
    public LayerMask enemiesMask;
    public GameObject LaserVFX;
    //todo public GameObject MuzzleParticlePref;
    public int NumOfRicochets { get; set; }

    public void FireWeapon(GameObject bulletPref, float dmg, int numofricochets)
    {
        Instantiate(LaserVFX, shootingFromTransform.position, transform.rotation);
        //Debug.DrawRay(transform.position, shootingFromTransform.up, Color.blue);
        var hit = Physics2D.Raycast(transform.position, shootingFromTransform.up, maxRayCastRange, enemiesMask);
        if (hit && hit.collider.transform.CompareTag("Enemy"))
        {
            hit.collider.GetComponent<IHaveHealth>().GetDamage(dmg);
        }
        AudioManager.Instance.PlaySound2D("Laser_Shoot");
    }
}
