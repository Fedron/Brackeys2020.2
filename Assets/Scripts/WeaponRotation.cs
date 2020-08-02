using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Vector2 mouseVector = Vector2.zero;


    public void SetMouseDirection(Vector3 dir)
    {
        mouseVector = dir;
    }

    private void Update()
    {
        Vector2 lookDir = mouseVector - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0,0,angle);
            
    }

}
