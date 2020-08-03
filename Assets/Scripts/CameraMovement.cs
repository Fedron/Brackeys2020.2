using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset = new Vector3(0f, 0f, -10f);

    Transform player;

    private void Awake() {
        player = FindObjectOfType<PlayerInputHandler>().transform;
    }

    private void LateUpdate() {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
