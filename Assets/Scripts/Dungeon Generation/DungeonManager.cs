using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {
    [Tooltip("Half-extends of a single room")]
    public int roomSize = 10;

    [Header("Enemies")]
    public int minEnemies;
    public int maxEnemies;
    public GameObject[] enemies;

    [Header("Room Templates")]
    public GameObject[] topRooms;
    public GameObject[] rightRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;

    [Space] public GameObject[] roomInners;
    public GameObject closedRoom;

    [HideInInspector] public List<GameObject> rooms = new List<GameObject>();
    [HideInInspector] public int activeRoom;

    bool roomContentGenerated = false;
    public delegate void GenerateRoomContent();
    public GenerateRoomContent generateRoomContent;

    public delegate void OpenDoors();
    public OpenDoors openDoors;
    public delegate void CloseDoors();
    public CloseDoors closeDoors;

    private void Awake() {
        activeRoom = 0;
    }

    private void LateUpdate() {
        if (roomContentGenerated) return;

        RoomSpawner[] spawners = GameObject.FindObjectsOfType<RoomSpawner>();
        if (spawners.Length == 0) {
            generateRoomContent?.Invoke();
            roomContentGenerated = true;
            openDoors?.Invoke();
        }
    }
}
