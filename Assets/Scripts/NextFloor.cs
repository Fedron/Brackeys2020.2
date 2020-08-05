using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloor : InteractableText
{

    private void Update() {
        if (interactableText.activeInHierarchy && Input.GetKeyDown(KeyCode.E)) {
            GameController.Instance.GoToNextFloor();
        }
    }

}
