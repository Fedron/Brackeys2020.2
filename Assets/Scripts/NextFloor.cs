using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloor : MonoBehaviour {
    [SerializeField] GameObject interactableText = default;

    private void Update() {
        if (interactableText.activeInHierarchy && Input.GetKeyDown(KeyCode.E)) {
            GameController.Instance.GoToNextFloor();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i)) {
            interactableText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.TryGetComponent<PlayerInputHandler>(out PlayerInputHandler i)) {
            interactableText.SetActive(false);
        }
    }
}
