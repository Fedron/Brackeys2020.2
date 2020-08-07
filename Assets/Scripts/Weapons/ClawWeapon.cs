using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawWeapon : MonoBehaviour, IShootingWeapon
{
    //todo public GameObject MuzzleParticlePref;
    public int NumOfRicochets { get; set; }
    public float SecondsForAnimation;
    public Animator animator;
    public void FireWeapon(GameObject bulletPref, float dmg, int numofricochets)
    {
        StartCoroutine(FiringWithAnimationTime(dmg));
    }

    IEnumerator FiringWithAnimationTime(float dmg)
    {
        //animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(SecondsForAnimation);
        //animator.SetBool("isAttacking", false);
        FindObjectOfType<PlayerInputHandler>().GetComponent<IHaveHealth>().GetDamage(dmg);
    }
}
