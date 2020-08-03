using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {
    [SerializeField] DungeonPreset[] dungeonPresets = default;
    int currentDungeonPreset = -1;

    [Header("Minimap Icons")]
    [SerializeField] GameObject startRoomMinimapIcon = default;
    [SerializeField] GameObject endRoomMinimapIcon = default;
    public GameObject chestRoomMinimapIcon = default;

    public GameObject nextFloorPrefab;

    [HideInInspector] public List<GameObject> rooms = new List<GameObject>();
    [HideInInspector] public int activeRoom;

    bool roomContentGenerated = false;
    public delegate void GenerateRoomContent();
    public GenerateRoomContent generateRoomContent;

    public delegate void OpenDoors();
    public OpenDoors openDoors;
    public delegate void CloseDoors();
    public CloseDoors closeDoors;

#region Dungeon Preset
    [HideInInspector] public int roomSize;
    [HideInInspector] public int minEnemies;
    [HideInInspector] public int maxEnemies;
    [HideInInspector] public GameObject[] enemies;
    [HideInInspector] public GameObject[] topRooms;
    [HideInInspector] public GameObject[] rightRooms;
    [HideInInspector] public GameObject[] bottomRooms;
    [HideInInspector] public GameObject[] leftRooms;
    [HideInInspector] public GameObject[] roomInners;
    [HideInInspector] public GameObject startRoom;
    [HideInInspector] public GameObject closedRoom;
#endregion

    private void Awake() {
        activeRoom = 0;
        GenerateDungeon();        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateDungeon();
    }

    private void LateUpdate() {
        if (roomContentGenerated) return;

        RoomSpawner[] spawners = GameObject.FindObjectsOfType<RoomSpawner>();
        if (spawners.Length == 0) {
            Instantiate(startRoomMinimapIcon, rooms[0].transform.position, Quaternion.identity, transform);
            Instantiate(endRoomMinimapIcon, rooms[rooms.Count - 1].transform.position, Quaternion.identity, transform);
            Instantiate(nextFloorPrefab, rooms[rooms.Count - 1].transform.position, Quaternion.identity, transform);

            generateRoomContent?.Invoke();
            roomContentGenerated = true;
            openDoors?.Invoke();
        }
    }

    public void GenerateDungeon() {
        // Prevent regeneration if current dungeon is still generating
        RoomSpawner[] spawners = GameObject.FindObjectsOfType<RoomSpawner>();
        if (spawners.Length > 0) return;

        // Destroy old dungeon if there was one
        if (rooms.Count > 0) {
            foreach (GameObject room in rooms) {
                Destroy(room);
            }
            for (int i = 0; i < transform.childCount; i++) {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        // Reset values
        rooms = new List<GameObject>();
        roomContentGenerated = false;
        activeRoom = 0;
        FindObjectOfType<PlayerInputHandler>().transform.position = Vector3.zero;

        currentDungeonPreset++;
        roomSize = dungeonPresets[currentDungeonPreset].roomSize;
        minEnemies = dungeonPresets[currentDungeonPreset].minEnemies;
        maxEnemies = dungeonPresets[currentDungeonPreset].maxEnemies;
        enemies = dungeonPresets[currentDungeonPreset].enemies;
        topRooms = dungeonPresets[currentDungeonPreset].topRooms;
        rightRooms = dungeonPresets[currentDungeonPreset].rightRooms;
        bottomRooms = dungeonPresets[currentDungeonPreset].bottomRooms;
        leftRooms = dungeonPresets[currentDungeonPreset].leftRooms;
        roomInners = dungeonPresets[currentDungeonPreset].roomInners;
        startRoom = dungeonPresets[currentDungeonPreset].startRoom;
        closedRoom = dungeonPresets[currentDungeonPreset].closedRoom;

        // Create new start room and therefore dungeon
        rooms.Add(Instantiate(startRoom, Vector3.zero, Quaternion.identity));
    }
}
