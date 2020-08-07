using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooting : MonoBehaviour, IShootingWeapon
{
    [SerializeField] AudioClip sound = default;
    public Transform[] shootingFromTransform;
    //todo public GameObject MuzzleParticlePref;
    public int NumOfRicochets { get; set; }

    public void FireWeapon(GameObject bulletPref, float dmg, int numofricochets)
    {
        //todo spawn a particle effect for fire muzzle
        //Instantiate(MuzzleParticlePref, shootingFromTransform[0].position, shootingFromTransform[0].rotation);
        foreach (Transform spawnPoint in shootingFromTransform)
        {
            var bullet = Instantiate(bulletPref, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Bullet>().Damage = dmg;
            bullet.GetComponent<Bullet>().maxNumofRicochets = numofricochets;
        }
        SoundManager.Instance.Play(sound, true);
    }
}
