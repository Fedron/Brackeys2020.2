using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {
    [SerializeField] bool debug = false;
    [SerializeField] DungeonPreset[] dungeonPresets = default;

    [Header("Minimap Icons")]
    [SerializeField] GameObject startRoomMinimapIcon = default;
    [SerializeField] GameObject endRoomMinimapIcon = default;
    public GameObject chestRoomMinimapIcon = default;

    public GameObject nextFloorPrefab;

    [HideInInspector] public List<GameObject> rooms = new List<GameObject>();
    [HideInInspector] public int activeRoom;

    [HideInInspector] public bool roomContentGenerated = false;
    public delegate void GenerateRoomContent();
    public GenerateRoomContent generateRoomContent;

    public delegate void OpenDoors();
    public OpenDoors openDoors;
    public delegate void CloseDoors();
    public CloseDoors closeDoors;

    private int activePreset = 0;

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
    }

    private void LateUpdate() {
        if (roomContentGenerated) return;

        //Debug.Log(GameObject.FindObjectsOfType<RoomSpawner>().Length);

        RoomSpawner[] spawners = GameObject.FindObjectsOfType<RoomSpawner>();
        if (spawners.Length > 0) return;
        roomContentGenerated = true;

        if (debug) Debug.Log("Layout complete, spawning minimap icons");
        Instantiate(startRoomMinimapIcon, rooms[0].transform.position, Quaternion.identity, transform);
        Instantiate(endRoomMinimapIcon, rooms[rooms.Count - 1].transform.position, Quaternion.identity, transform);
        Instantiate(nextFloorPrefab, rooms[rooms.Count - 1].transform.position, Quaternion.identity, transform);

        //Debug.Log(generateRoomContent.GetInvocationList().Length);
        //generateRoomContent?.Invoke();
        foreach (GameObject room in rooms) {
            room.GetComponent<RoomManager>().SpawnContent();
        }

        // if (debug) Debug.Log("------ Generating room content ------");
        // generateRoomContent?.Invoke();
        // roomContentGenerated = true;
        // if (debug) Debug.Log("------ Generation complete, opening doors ------");
        // openDoors?.Invoke();
    }

    public void GenerateDungeon(int preset = -1) {
        // Prevent regeneration if current dungeon is still generating
        RoomSpawner[] spawners = GameObject.FindObjectsOfType<RoomSpawner>();
        if (spawners.Length > 0) return;

        if (debug) Debug.Log("------ Destroying old dungeon ------");
        foreach (GameObject room in rooms) {
            Destroy(room);
        }
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        // Reset values
        if (debug) Debug.Log("Reseting values");
        rooms = new List<GameObject>();
        roomContentGenerated = false;
        activeRoom = 0;
        FindObjectOfType<PlayerInputHandler>().transform.position = Vector3.zero;

        // Add an animation for the dungeon falling and regenerating around the player
        // Currently it just appears out of thin air

        if (preset != -1) activePreset = Mathf.Clamp(preset, 0, dungeonPresets.Length);
        if (debug) Debug.Log(string.Concat("Setting preset values using preset ", activePreset));

        roomSize = dungeonPresets[activePreset].roomSize;
        minEnemies = dungeonPresets[activePreset].minEnemies;
        maxEnemies = dungeonPresets[activePreset].maxEnemies;
        enemies = dungeonPresets[activePreset].enemies;
        topRooms = dungeonPresets[activePreset].topRooms;
        rightRooms = dungeonPresets[activePreset].rightRooms;
        bottomRooms = dungeonPresets[activePreset].bottomRooms;
        leftRooms = dungeonPresets[activePreset].leftRooms;
        roomInners = dungeonPresets[activePreset].roomInners;
        startRoom = dungeonPresets[activePreset].startRoom;
        closedRoom = dungeonPresets[activePreset].closedRoom;

        // Create new start room and therefore dungeon
        if (debug) Debug.Log("------ Starting Dungeon Generation ------");
        if (debug) Debug.Log("Instantiating rooms and generating layout");
        rooms.Add(Instantiate(startRoom, Vector3.zero, Quaternion.identity, transform));
    }
}
