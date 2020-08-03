using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : MonoBehaviour
{
    public GameObject weaponPref;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.BroadcastMessage("RemoveWeapon");
            Instantiate(weaponPref, collision.transform.position, Quaternion.identity, collision.transform);
            Destroy(gameObject);
        }
        
    }
}
