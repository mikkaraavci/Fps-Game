using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadcastMessage : MonoBehaviour
{
    public string message;
    public bool sendMessageOnce;
    private bool messageSent;


    public void SendMessageToUI()
    {
        if (!messageSent)
        {
            GameObject userInterface = GameObject.FindGameObjectWithTag("UserInterface");
            userInterface.GetComponent<UIManager>().SetMessageText(message);
        }
        
        if (sendMessageOnce)
        {
            messageSent = true;
        }
        

    }

}
