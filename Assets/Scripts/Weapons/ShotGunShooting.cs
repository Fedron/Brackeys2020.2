using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooting : MonoBehaviour, IShootingWeapon
{
    public Transform shootingFromTransform;

    public void FireWeapon(GameObject bulletPref)
    {
        var bullet = Instantiate(bulletPref, transform.position, Quaternion.identity, transform);
    }
}
