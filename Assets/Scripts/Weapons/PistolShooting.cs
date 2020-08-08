using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShooting : MonoBehaviour, IShootingWeapon
{
    public Transform shootingFromTransform;
    public GameObject MuzzleParticlePref;
    public int NumOfRicochets { get; set; }

    public void FireWeapon(GameObject bulletPref, float dmg, int numofricochets)
    {
        Instantiate(MuzzleParticlePref, shootingFromTransform.position, transform.rotation);
        var bullet = Instantiate(bulletPref, shootingFromTransform.position, shootingFromTransform.rotation);
        bullet.GetComponent<Bullet>().Damage = dmg;
        bullet.GetComponent<Bullet>().maxNumofRicochets = numofricochets;
        AudioManager.Instance.PlaySound2D("Pistol");
    }
}
