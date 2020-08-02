using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShooting : MonoBehaviour, IShootingWeapon
{
    public Transform shootingFromTransform;

    public void FireWeapon(GameObject bulletPref, float dmg)
    {
        var bullet = Instantiate(bulletPref, shootingFromTransform.position, shootingFromTransform.rotation);
        bullet.GetComponent<Bullet>().Damage = dmg;
    }
}
