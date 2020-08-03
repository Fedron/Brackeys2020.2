using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rewindable : MonoBehaviour {
    [Tooltip("A lower value more closely matches the saved velocities")]
    [SerializeField, Range(0.01f, 1f)] float fidelity = 0.5f;
    [SerializeField] float secondsToSave = 5f;

    FixedStack<Vector2> velocities;
    new Rigidbody2D rigidbody;

    [HideInInspector] public bool rewinding = false;

    public delegate void RewindComplete();
    public RewindComplete rewindComplete;

    // Can moved to its own script, but it's only ever used inside this script atm
    private class FixedStack<T> : LinkedList<T> {
        private int _maxSize;
        public FixedStack(int maxSize) {
            _maxSize = maxSize;
        }

        public void Push(T item) {
            this.AddFirst(item);

            if(this.Count > _maxSize)
                this.RemoveLast();
        }

        public T Pop() {
            var item = this.First.Value;
            this.RemoveFirst();
            return item;
        }
    }

    private void Awake() {
        FindObjectOfType<RewinderManager>().normalRevive += Rewind;

        velocities = new FixedStack<Vector2>(Mathf.RoundToInt(secondsToSave / fidelity));
        rigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("SaveVelocity", 0.00001f, fidelity);
    }

    private void SaveVelocity() {
        velocities.Push(rigidbody.velocity);
    }

    public void Rewind() {
        rewinding = true;
        CancelInvoke("SaveVelocity");
        InvokeRepeating("PlaybackVelocities", 0.00001f, fidelity);
    }

    private void PlaybackVelocities() {
        if (velocities.Count == 0) {
            rewinding = false;
            CancelInvoke("PlaybackVelocities");
            rewindComplete?.Invoke();
            rigidbody.velocity = Vector2.zero;
            return;
        }
        Vector2 velocity = velocities.Pop() * -1f;
        rigidbody.velocity = velocity;
    }
}
