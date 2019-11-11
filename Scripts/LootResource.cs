using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootResource : MonoBehaviour
{
    public string loot;
    public bool addOnce;

    public void AddLootToPlayerInventory()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (addOnce)
            player.GetComponent<PlayerInventory>().AddItemOnceToInventory(loot);
        else
            player.GetComponent<PlayerInventory>().AddItemToInventory(loot);
    }
}
