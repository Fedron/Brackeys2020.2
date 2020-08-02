using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {
    private enum CorridorDirection {
        Top,
        Right,
        Bottom,
        Left
    }
    [SerializeField] CorridorDirection corridorDirection;

    DungeonManager dungeon;
    [HideInInspector] public bool spawned = false;

    private void Awake() {
        Destroy(gameObject, 1f);
        dungeon = GameObject.FindGameObjectWithTag("Rooms").GetComponent<DungeonManager>();
        Invoke("Spawn", 0.05f);
    }

    private void Spawn() {
        if (!spawned) {
            if (corridorDirection == CorridorDirection.Top) {
                Instantiate(
                    dungeon.bottomRooms[Random.Range(0, dungeon.bottomRooms.Length)],
                    transform.position,
                    Quaternion.identity,
                    dungeon.transform
                );
            } else if (corridorDirection == CorridorDirection.Right) {
                Instantiate(
                    dungeon.leftRooms[Random.Range(0, dungeon.leftRooms.Length)],
                    transform.position,
                    Quaternion.identity,
                    dungeon.transform
                );
            } else if (corridorDirection == CorridorDirection.Bottom) {
                Instantiate(
                    dungeon.topRooms[Random.Range(0, dungeon.topRooms.Length)],
                    transform.position,
                    Quaternion.identity,
                    dungeon.transform
                );
            }  else if (corridorDirection == CorridorDirection.Left) {
                Instantiate(
                    dungeon.rightRooms[Random.Range(0, dungeon.rightRooms.Length)],
                    transform.position,
                    Quaternion.identity,
                    dungeon.transform
                );
            }
            spawned = true;
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("RoomSP")) {
            try {
                if (!other.GetComponent<RoomSpawner>().spawned && !spawned && transform.position != Vector3.zero) {
                    Instantiate(dungeon.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            } catch {
                Destroy(gameObject);
            }     
            spawned = true;
        }
    }
}