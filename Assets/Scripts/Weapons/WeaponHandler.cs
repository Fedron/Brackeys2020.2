using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private IShootingWeapon weaponShootingBehavior;
    public WeaponStats weaponStats;
    [SerializeField] 
    private float fireRefreshRate, damage;
    private GameObject bulletPref, pickableWeaponPref;
    private float nextFireTime = 0f;
    public int NumOfRicochets;

    private void Awake()
    {
        fireRefreshRate = weaponStats.coolDownRate;
        damage = weaponStats.damage;
        bulletPref = weaponStats.bulletPrefab;
        pickableWeaponPref = weaponStats.pickableWeaponPref;
        weaponShootingBehavior = GetComponent<IShootingWeapon>();
    }

    // Calls for FireWeapon method
    public void Fire()
    {
        if (CanFire())
        {
            // Resets the timer for cooldown
            nextFireTime = Time.time + fireRefreshRate;
            weaponShootingBehavior.FireWeapon(bulletPref, damage, NumOfRicochets);
        }
    }
    public void RemoveWeapon()
    {
        Instantiate(pickableWeaponPref, (Vector2)transform.position + new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)),
                Quaternion.identity);
        Destroy(gameObject);
    }

    // Adds a cooldown
    private bool CanFire()
    {
        return Time.time >= nextFireTime;
    }
}
