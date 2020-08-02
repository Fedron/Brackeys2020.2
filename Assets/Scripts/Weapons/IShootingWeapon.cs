using UnityEngine;

public interface IShootingWeapon
{
    void FireWeapon(GameObject bulletPref, float damage, int numofricochets);
}