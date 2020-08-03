using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dungeon Preset", menuName = "Dungeon Preset")]
public class DungeonPreset : ScriptableObject {
    [Space, Tooltip("Half-extends of a single room")]
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
    public GameObject startRoom;
    public GameObject closedRoom;
}
