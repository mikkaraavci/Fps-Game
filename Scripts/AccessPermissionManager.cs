using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessPermissionManager : MonoBehaviour
{
    public string itemToCheck;
    public string messageForFalse;
    public GameObject carriedItem;
    private bool canGetItem;
   

    public bool CheckItemInInventory(GameObject player)
    {
        bool permit = player.GetComponent<PlayerInventory>().CheckInventory(itemToCheck);
        if (!permit)
        {
            GameObject userInterface = GameObject.FindGameObjectWithTag("UserInterface");
            userInterface.GetComponent<UIManager>().SetMessageText(messageForFalse);
        } else
        {
            if (carriedItem != null)
            {
                GameObject userInterface = GameObject.FindGameObjectWithTag("UserInterface");
                userInterface.GetComponent<UIManager>().SetMessageText("Press F to get weapon");
                canGetItem = true;

            }
        }
        return permit;

    }

    private void Update()
    {
        if (canGetItem)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // weapon'i playera gönder
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Transform weaponDummy = player.transform.Find("WeaponDummyObject");
                carriedItem.transform.SetParent(weaponDummy);
                carriedItem.transform.localPosition = Vector3.zero;
                carriedItem.transform.localRotation = Quaternion.identity;
                if (carriedItem.GetComponent<WeaponController>())
                   
                {
                    carriedItem.GetComponent<WeaponController>().inUse = true;
                    carriedItem.GetComponent<WeaponController>().onPlayer = true;
                }
                carriedItem = null;
                canGetItem = false;

            }
        }    
    }
}
