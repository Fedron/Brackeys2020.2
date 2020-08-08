using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShooting : MonoBehaviour, IShootingWeapon
{
    public Transform[] shootingFromTransform;
    //todo public GameObject MuzzleParticlePref;
    public int NumOfRicochets { get; set; }
    public float SecondsForAnimation;
    public Animator animator;

    public void FireWeapon(GameObject bulletPref, float dmg, int numofricochets)
    {
        StartCoroutine(FiringWithAnimationTime(bulletPref, dmg, numofricochets));
    }

    IEnumerator FiringWithAnimationTime(GameObject bulletPref, float dmg, int numofricochets)
    {
        animator.SetBool("isShooting", true);
        yield return new WaitForSeconds(SecondsForAnimation);
        animator.SetBool("isShooting", false);

        //todo spawn a particle effect for fire muzzle
        //Instantiate(MuzzleParticlePref, shootingFromTransform[0].position, shootingFromTransform[0].rotation);
        foreach (Transform spawnPoint in shootingFromTransform)
        {
            var bullet = Instantiate(bulletPref, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Bullet>().Damage = dmg;
            bullet.GetComponent<Bullet>().maxNumofRicochets = numofricochets;
        }
        AudioManager.Instance.PlaySound2D("Fire_ElementalShoot");
    }
}
