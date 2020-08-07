using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableHealthKit : InteractableText
{
    [SerializeField] AudioClip sound = default;
    Collider2D tempCollision;
    public float HealAmount;

    private void Update()
    {
        if (interactableText.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            tempCollision.GetComponent<IHaveHealth>().Heal(HealAmount);
            SoundManager.Instance.Play(sound, true);
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
