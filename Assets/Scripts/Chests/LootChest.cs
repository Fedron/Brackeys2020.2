using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The most simple treasure chest script ever made :P basically on game start it spawns items in a straight line.
/// </summary>
[System.Serializable]
public class LootChest : MonoBehaviour
{
    // Loot drop table that contains items that can spawn
    public LootDropTableGameObject lootDropTable;

    // How many items treasure will spawn
    public int numItemsToDrop;

    void OnValidate()
    {

        // Validate table and notify the programmer / designer if something went wrong.
        lootDropTable.ValidateTable();

    }

    /// <summary>
    /// Spawning objects in horizontal line
    /// </summary>
    /// <param name="numItemsToDrop"></param>
    public void DropLootNearChest()
    {
        for (int i = 0; i < numItemsToDrop; i++)
        {
            LootDropItemGameObject selectedItem = lootDropTable.PickLootDropItem();
            GameObject selectedItemGameObject = Instantiate(selectedItem.item, 
                (Vector2)transform.position + new Vector2(Random.Range(-1, 4), Random.Range(-1, 4)), 
                Quaternion.identity, transform);

        }
    }
}

