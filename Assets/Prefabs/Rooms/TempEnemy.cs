using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour {
    CreaturesHealth ch;

    private void Awake() {
        ch = GetComponent<CreaturesHealth>();
        ch.OnDie += Die;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet)) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
