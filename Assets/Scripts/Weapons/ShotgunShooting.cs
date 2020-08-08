using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooting : MonoBehaviour, IShootingWeapon
{
    public Transform[] shootingFromTransform;
    public GameObject MuzzleParticlePref;
    public int NumOfRicochets { get; set; }

    public void FireWeapon(GameObject bulletPref, float dmg, int numofricochets)
    {
        Instantiate(MuzzleParticlePref, shootingFromTransform[0].position, transform.rotation);
        foreach (Transform spawnPoint in shootingFromTransform)
        {
            var bullet = Instantiate(bulletPref, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Bullet>().Damage = dmg;
            bullet.GetComponent<Bullet>().maxNumofRicochets = numofricochets;
            AudioManager.Instance.PlaySound2D("Shotgun");
        }
    }
}
