using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewindable : MonoBehaviour
{
    Stack<Vector2> savedVelocities = new Stack<Vector2>();
    new Rigidbody2D rigidbody;
    // todo Finish this script
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(RecordVelocities());
    }

    IEnumerator RecordVelocities()
    {
        while (true)
        {
            savedVelocities.Push(-rigidbody.velocity);
            yield return new WaitForSeconds(1f);
        }
    }

    private void InvertVelocities()
    {
        StopCoroutine(RecordVelocities());
        rigidbody.velocity = savedVelocities.Pop();
    }
}
