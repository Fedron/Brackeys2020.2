using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    [SerializeField] GameObject minimapWalls = default;

    [SerializeField] public DungeonManager dungeon;
    int enemiesToSpawn;

    public int EnemyCount {
        get { return enemies.Count; }
    }
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public int roomID;
    [HideInInspector] public bool explored;
    bool enemiesActivated = false;

    private void Awake() {
        dungeon = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
        if (!dungeon.rooms.Contains(gameObject)) dungeon.rooms.Add(gameObject);

        roomID = dungeon.rooms.IndexOf(gameObject);
        if (roomID == 0) ShowOnMinimap();

        dungeon.generateRoomContent += SpawnContent;
    }

    private void LateUpdate() {
        if (dungeon.activeRoom != roomID) return;

        if (EnemyCount > 0) {
            if (AstarPath.active.data.gridGraph.center != transform.position) {
                AstarPath.active.data.gridGraph.center = transform.position;
                AstarPath.active.Scan(AstarPath.active.data.gridGraph);
            }

            if (!enemiesActivated) {
                enemiesActivated = true;
                foreach (GameObject enemy in enemies) {
                    enemy.GetComponent<AIMoverandPathfinding>().enabled = true;
                }
            }

            List<GameObject> oldEnemies = enemies;
            for (int i = 0; i < oldEnemies.Count; i++) {
                if (enemies[i] == null) enemies.Remove(oldEnemies[i]);
            }
        } else {
            if (dungeon.roomContentGenerated) dungeon.openDoors?.Invoke();
        }
    }

    public void SpawnContent() {
        // Prevent start and end rooms from generating content
        if (roomID == 0 || roomID == dungeon.rooms.Count - 1) return;
        enemiesToSpawn = Random.Range(dungeon.minEnemies, dungeon.maxEnemies + 1);

        // Spawn Inners
        Instantiate(
            dungeon.roomInners[Random.Range(0, dungeon.roomInners.Length)],
            transform.position,
            Quaternion.Euler(0f, 0f, 90f * Random.Range(0, 4)),
            transform
        );

        // Spawn Enemies
        for (int i = 0; i < enemiesToSpawn; i++) {
            Vector3 spawnPos;
            do {
                spawnPos = new Vector3(
                    transform.position.x + Random.Range(-dungeon.roomSize + 1, dungeon.roomSize - 1),
                    transform.position.y + Random.Range(-dungeon.roomSize + 1, dungeon.roomSize - 1),
                    0f
                );
            } while (Physics2D.OverlapCircle(spawnPos, 0.75f) != null);

            GameObject enemy = Instantiate(
                dungeon.dungeonPresets[dungeon.previousPreset].enemies[Random.Range(0, dungeon.dungeonPresets[dungeon.previousPreset].enemies.Length)],
                spawnPos, Quaternion.identity
            );
            enemies.Add(enemy);
        }
        dungeon.generateRoomContent -= SpawnContent;
    }

    public void ShowOnMinimap() {
        if (explored) return;
        explored = true;
        minimapWalls.SetActive(true);
        
        if (transform.GetChild(transform.childCount - 1).name.Contains("Chest"))
            Instantiate(dungeon.chestRoomMinimapIcon, transform.position, Quaternion.identity, transform);
    }

    private void OnDestroy() {
        dungeon.generateRoomContent -= SpawnContent;
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }
}