using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : InteractableText
{
    public GameObject weaponPref;
    Collider2D tempCollision;

    private void Update()
    {
        if (interactableText.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            tempCollision.BroadcastMessage("RemoveWeapon");
            Instantiate(weaponPref, tempCollision.transform.position, Quaternion.identity, tempCollision.transform);
            Destroy(gameObject);
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i))
        {
            tempCollision = collision;
            interactableText.SetActive(true);

        }

    }
}
