using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> inventory = new List<string>();

    public void AddItemToInventory(string anItemName)
    {
        inventory.Add(anItemName);
        GameObject userInterface = GameObject.FindGameObjectWithTag("UserInterface");
        userInterface.GetComponent<UIManager>().SetMessageText("Added item " + anItemName + " to inventory");
    }

    public void AddItemOnceToInventory(string anItemName)
    {
        if (!CheckInventory(anItemName))
        {
            AddItemToInventory(anItemName);
        }
    }

    public bool CheckInventory(string anItemName)
    {
        //for (int i=0; i<inventory.Count; i++)
        //{
        //    if (inventory[i] == anItemName)
        //    {
        //        return true;
        //    }
        //}
        //return false;
        if (inventory.Contains(anItemName))
        {
            return true;
        }
        return false;
    }

    public void RemoveItemFromInventory(string anItemName)
    {
        if (CheckInventory(anItemName))
        {
            inventory.Remove(anItemName);
        }
    }
}
