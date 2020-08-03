using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleText : MonoBehaviour
{
    public GameObject interactableText = default;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i))
        {
            interactableText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i))
        {
            interactableText.SetActive(false);
        }
    }
}
