using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleChests : InteractableText
{
    LootChest lootchest;
    bool opened = false;

    private void Awake()
    {
        lootchest = GetComponent<LootChest>();
    }
    private void Update()
    {
        if (interactableText.activeInHierarchy && Input.GetKeyDown(KeyCode.E) && !opened)
        {
            lootchest.DropLootNearChest();
            opened = true;
            //todo swap sprite image to a opened chest
            GetComponent<Animator>().SetTrigger("Open");
            interactableText.SetActive(false);
            // Removes this script from the chest
            Destroy(this);
        }
    }
}
