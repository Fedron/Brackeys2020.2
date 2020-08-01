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
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");
        playerMover.SetDirection(new Vector2(horizontal, vertical));
        playerMover.SetMouseDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetButtonDown("Fire1")) { BroadcastMessage("Fire"); }
    }
}
