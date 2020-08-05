using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableText : MonoBehaviour
{
    public GameObject interactableText = default;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i))
        {
            interactableText.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i))
        {
            interactableText.SetActive(false);
        }
    }
}
