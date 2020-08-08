using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    [SerializeField] GameObject minimapWalls = default;
    [SerializeField] Sprite[] doorSprites = default;
    [HideInInspector] public Sprite doorSprite = default;

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
        doorSprite = doorSprites[Random.Range(0, doorSprites.Length)];

        dungeon = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
        if (!dungeon.rooms.Contains(gameObject)) dungeon.rooms.Add(gameObject);

        roomID = dungeon.rooms.IndexOf(gameObject);
        if (roomID == 0) ShowOnMinimap();

        dungeon.generateRoomContent += SpawnContent;
        dungeon.destroyRooms += DestroyRoom;
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
                    try {
                        enemy.GetComponent<AIMoverandPathfinding>().enabled = true;
                    } catch {}                    
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
            dungeon.dungeonPresets[dungeon.previousPreset].roomInners[Random.Range(0, dungeon.dungeonPresets[dungeon.previousPreset].roomInners.Length)],
            transform.position,
            Quaternion.Euler(0f, 0f, 90f * Random.Range(0, 4)),
            transform
        );
        if (transform.GetChild(transform.childCount - 1).name.Contains("Chest"))
            transform.GetChild(transform.childCount - 1).rotation = Quaternion.Euler(Vector3.zero);

        // Spawn Enemies
        for (int i = 0; i < enemiesToSpawn; i++) {
            Vector3 spawnPos;
            int iters = 0;
            do {
                iters++;
                spawnPos = new Vector3(
                    transform.position.x + Random.Range(-dungeon.roomSize + 1, dungeon.roomSize - 1),
                    transform.position.y + Random.Range(-dungeon.roomSize + 1, dungeon.roomSize - 1),
                    0f
                );
            } while (Physics2D.OverlapCircle(spawnPos, 0.75f, LayerMask.NameToLayer("Ground")) != null && iters < 3);
            if (spawnPos == null) continue;
            
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

    public void DestroyRoom() {
        dungeon.generateRoomContent -= SpawnContent;
        dungeon.destroyRooms -= DestroyRoom;

        GameObject[] enemiesToDestroy = enemies.ToArray();
        for (int i = 0; i < enemiesToDestroy.Length; i++) {
            Destroy(enemiesToDestroy[i]);
        }
        Destroy(gameObject);
    }
}