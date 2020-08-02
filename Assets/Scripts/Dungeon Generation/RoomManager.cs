using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    DungeonManager dungeon;
    int enemiesToSpawn;

    public int EnemyCount {
        get { return enemies.Count; }
    }
    List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public int roomID;

    private void Awake() {
        dungeon = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();
        dungeon.rooms.Add(gameObject);
        roomID = dungeon.rooms.IndexOf(gameObject);
        dungeon.generateRoomContent += SpawnContent;
    }

    private void LateUpdate() {
        if (dungeon.activeRoom != roomID) return;

        if (EnemyCount > 0) {
            List<GameObject> oldEnemies = enemies;
            for (int i = 0; i < oldEnemies.Count; i++) {
                if (enemies[i] == null) enemies.Remove(oldEnemies[i]);
            }
        } else {
            dungeon.openDoors?.Invoke();
        }
    }

    private void SpawnContent() {
        // Prevent start and end rooms from generating content
        int roomIndex = dungeon.rooms.IndexOf(gameObject);
        if (roomIndex == 0 || roomIndex == dungeon.rooms.Count - 1) return;
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
            } while (Physics2D.OverlapCircle(spawnPos, 1f) != null);

            GameObject enemy = Instantiate(
                dungeon.enemies[Random.Range(0, dungeon.enemies.Length)],
                spawnPos, Quaternion.identity
            );
            enemies.Add(enemy);
        }
        dungeon.generateRoomContent -= SpawnContent;
    }
}