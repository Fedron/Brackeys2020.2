using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private IShootingWeapon weaponShootingBehavior;
    public WeaponStats weaponStats;
    [SerializeField] 
    private float fireRefreshRate, damage;
    private GameObject bulletPref;
    private float nextFireTime = 0f;

    private void Awake()
    {
        fireRefreshRate = weaponStats.coolDownRate;
        damage = weaponStats.damage;
        bulletPref = weaponStats.bulletPrefab;
        weaponShootingBehavior = GetComponent<IShootingWeapon>();
    }

    // Calls for FireWeapon method
    public void Fire()
    {
        if (CanFire())
        {
            // Resets the timer for cooldown
            nextFireTime = Time.time + fireRefreshRate;
            weaponShootingBehavior.FireWeapon(bulletPref);
        }
    }

    // Adds a cooldown
    private bool CanFire()
    {
        return Time.time >= nextFireTime;
    }
}
