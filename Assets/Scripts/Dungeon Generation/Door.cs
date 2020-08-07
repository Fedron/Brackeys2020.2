using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] RoomManager room = default;
    [SerializeField] Animator animator = default;

    [Space, SerializeField] SpriteRenderer left = default;
    [SerializeField] SpriteRenderer right = default;

    DungeonManager dungeon;

    private void Awake() {
        dungeon = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
        dungeon.openDoors += Open;
        dungeon.closeDoors += Close;

        left.sprite = room.doorSprite;
        right.sprite = room.doorSprite;
    }

    private void OnDisable() {
        dungeon.openDoors -= Open;
        dungeon.closeDoors -= Close;
    }

    public void Open() {
        animator.SetBool("open", true);
        animator.SetBool("closed", false);
    }

    public void Close() {
        animator.SetBool("open", false);
        animator.SetBool("closed", true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (dungeon.activeRoom != room.roomID && other.CompareTag("Player")) {
            dungeon.activeRoom = room.roomID;
            if (room.EnemyCount > 0 || !room.explored) {         
                dungeon.closeDoors?.Invoke();
                room.ShowOnMinimap();
            } else {
                dungeon.openDoors?.Invoke();
            }
        }
    }
}
