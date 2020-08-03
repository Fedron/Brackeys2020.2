using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMover playerMover;
    private Rewindable rewinder;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
        rewinder = GetComponent<Rewindable>();
    }
    private void Update()
    {
        if (rewinder.rewinding) return;

        // Used for testing, can be removed when player can die from enemy bullets
        if (Input.GetKeyDown(KeyCode.F)) GetComponent<CreaturesHealth>().Die();

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        playerMover.SetDirection(new Vector2(horizontal, vertical));
        //BroadcastingMessage in order for mouse movements to be seen by any weapon
        BroadcastMessage("SetMouseDirection", Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // BroadcastingMessage in order for any weapon to know that the shot was fired
        if (Input.GetButtonDown("Fire1")) { BroadcastMessage("Fire"); }
    }
}
