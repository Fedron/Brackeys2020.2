using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMover playerMover;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
    }
    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        playerMover.SetDirection(new Vector2(horizontal, vertical));
        //BroadcastingMessage in order for mouse movements to be seen by any weapon
        BroadcastMessage("SetMouseDirection", Camera.main.ScreenToWorldPoint(Input.mousePosition));
        // BroadcastingMessage in order for any weapon to know that the shot was fired
        if (Input.GetButtonDown("Fire1")) { BroadcastMessage("Fire"); }
    }
}
