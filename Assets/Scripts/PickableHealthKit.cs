using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableHealthKit : InteractableText
{
    Collider2D tempCollision;
    public float HealAmount;

    private void Update()
    {
        if (interactableText.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            tempCollision.GetComponent<IHaveHealth>().Heal(HealAmount);
            Destroy(gameObject);
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i))
        {
            tempCollision = collision;
            interactableText.SetActive(true);
            AudioManager.Instance.PlaySound2D("Heal_Powerup");
        }

    }
}
