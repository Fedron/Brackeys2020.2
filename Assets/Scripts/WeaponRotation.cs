using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Vector2 mouseVector = Vector2.zero;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void SetMouseDirection(Vector3 dir)
    {
        mouseVector = dir;
    }

    private void Update()
    {
        Vector2 lookDir = mouseVector - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270) { spriteRend.flipY = true; }
        else spriteRend.flipY = false;
    }

}
