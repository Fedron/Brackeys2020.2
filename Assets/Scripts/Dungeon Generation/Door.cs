using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] RoomManager room = default;
    [SerializeField] Animator animator = default;

    DungeonManager dungeon;

    private void Awake() {
        dungeon = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
        dungeon.openDoors += Open;
        dungeon.closeDoors += Close;
    }

    private void Open() {
        animator.SetBool("open", true);
        animator.SetBool("closed", false);
    }

    private void Close() {
        animator.SetBool("open", false);
        animator.SetBool("closed", true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (dungeon.activeRoom != room.roomID && other.CompareTag("Player")) {
            if (room.EnemyCount > 0) {
                dungeon.activeRoom = room.roomID;
                dungeon.closeDoors?.Invoke();
            }
            FindObjectOfType<CameraMovement>().MoveCamera(new Vector3(
                room.transform.position.x,
                room.transform.position.y,
                0f
            ));
        }
    }
}
