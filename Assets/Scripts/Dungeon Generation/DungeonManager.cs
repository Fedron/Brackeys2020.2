using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {
    [SerializeField] bool debug = false;
    [SerializeField] public DungeonPreset[] dungeonPresets = default;

    [Header("Minimap Icons")]
    [SerializeField] GameObject startRoomMinimapIcon = default;
    [SerializeField] GameObject endRoomMinimapIcon = default;
    public GameObject chestRoomMinimapIcon = default;

    public GameObject nextFloorPrefab;

    [Header("Schrodinger")]
    [SerializeField] Color cameraBackground = default;
    [SerializeField] GameObject backgroundFX = default;
    [SerializeField] GameObject regularMapBorder = default;
    [SerializeField] GameObject corruptedMapBorder = default;

    [HideInInspector] public List<GameObject> rooms = new List<GameObject>();
    [HideInInspector] public int activeRoom;

    [HideInInspector] public bool roomContentGenerated = false;
    // public delegate void GenerateRoomContent();
    // public GenerateRoomContent generateRoomContent;
    public event Action generateRoomContent = delegate { };

    public delegate void OpenDoors();
    public OpenDoors openDoors;
    public delegate void CloseDoors();
    public CloseDoors closeDoors;
    public delegate void DestroyRooms();
    public DestroyRooms destroyRooms;

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

    [HideInInspector] public int previousPreset = 0;
    int preset = 0;
    [Space, SerializeField] Animator animator = default;

    private void Awake() {
        activeRoom = 0;
    }

    private void LateUpdate() {
        if (roomContentGenerated) return;

        RoomSpawner[] spawners = GameObject.FindObjectsOfType<RoomSpawner>();
        if (spawners.Length > 0) return;
        roomContentGenerated = true;

        if (debug) Debug.Log("Layout complete, spawning minimap icons");
        try {
            Instantiate(startRoomMinimapIcon, rooms[0].transform.position, Quaternion.identity, transform);
            Instantiate(endRoomMinimapIcon, rooms[rooms.Count - 1].transform.position, Quaternion.identity, transform);
            Instantiate(nextFloorPrefab, rooms[rooms.Count - 1].transform.position, Quaternion.identity, transform);
        } catch {}        

        if (debug) Debug.Log("------ Generating room content ------");
        generateRoomContent?.Invoke();
    }

    public void GenerateDungeon(int pr = -1) {
        // Prevent regeneration if current dungeon is still generating
        RoomSpawner[] spawners = GameObject.FindObjectsOfType<RoomSpawner>();
        if (spawners.Length > 0) return;

        preset = pr;
        animator.SetTrigger("Regen");
        Invoke("Generate", 0.6f);
    }

    private void Generate() {
        if (debug) Debug.Log("------ Destroying old dungeon ------");
        if (rooms.Count > 0) {
            destroyRooms?.Invoke();
            GameObject[] icons = GameObject.FindGameObjectsWithTag("MinimapIcon");
            for (int i = 0; i < icons.Length; i++) {
                Destroy(icons[i].gameObject);
            }
        }

        // Reset values
        if (debug) Debug.Log("Reseting values");
        rooms = new List<GameObject>();
        System.Delegate.RemoveAll(generateRoomContent, generateRoomContent);
        System.Delegate.RemoveAll(destroyRooms, destroyRooms);
        roomContentGenerated = false;
        activeRoom = 0;
        FindObjectOfType<PlayerInputHandler>().transform.position = Vector3.zero;

        if (preset != -1) preset = Mathf.Clamp(preset, 0, dungeonPresets.Length);
        else preset = previousPreset;

        if (debug) Debug.Log(string.Concat("Setting preset values using preset ", preset));

        if (previousPreset != preset) {
            previousPreset = preset;
            roomSize = dungeonPresets[preset].roomSize;
            minEnemies = dungeonPresets[preset].minEnemies;
            maxEnemies = dungeonPresets[preset].maxEnemies;
            enemies = dungeonPresets[preset].enemies;
            topRooms = dungeonPresets[preset].topRooms;
            rightRooms = dungeonPresets[preset].rightRooms;
            bottomRooms = dungeonPresets[preset].bottomRooms;
            leftRooms = dungeonPresets[preset].leftRooms;
            roomInners = dungeonPresets[preset].roomInners;
            startRoom = dungeonPresets[preset].startRoom;
            closedRoom = dungeonPresets[preset].closedRoom;
        }       

        // Create new start room and therefore dungeon
        if (debug) Debug.Log("------ Starting Dungeon Generation ------");
        if (debug) Debug.Log("Instantiating rooms and generating layout");
        rooms.Add(Instantiate(startRoom, Vector3.zero, Quaternion.identity, transform));

        // Schrodinger visuals
        if (preset == 1) {
            Camera.main.backgroundColor = cameraBackground;
            backgroundFX.SetActive(true);
            regularMapBorder.SetActive(true);
            corruptedMapBorder.SetActive(true);
        }  
    }
}
