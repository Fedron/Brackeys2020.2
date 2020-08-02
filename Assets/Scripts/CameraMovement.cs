using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset = new Vector3(0f, 0f, -10f);

    bool moveCamera = false;
    Vector3 targetPos;

    private void LateUpdate() {
        if (!moveCamera) return;

        Vector3 desiredPosition = targetPos + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (Vector3.Distance(transform.position, targetPos) < 0.05f) {
            moveCamera = false;
        }
    }

    public void MoveCamera(Vector3 targetPos) {
        moveCamera = true;
        this.targetPos = targetPos;
    }
}
