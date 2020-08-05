using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleChests : InteractableText
{
    LootChest lootchest;
    SpriteRenderer spriteRenderer;
    bool opened = false;
    public Sprite openedChestSprite;

    private void Awake()
    {
        lootchest = GetComponent<LootChest>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (interactableText.activeInHierarchy && Input.GetKeyDown(KeyCode.E) && !opened)
        {
            lootchest.DropLootNearChest();
            opened = true;
            //todo swap sprite image to a opened chest
            spriteRenderer.sprite = openedChestSprite;
            interactableText.SetActive(false);
            // Removes this script from the chest
            Destroy(this);
        }
    }
}
