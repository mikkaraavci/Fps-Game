using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeStyleOpenClose : MonoBehaviour
{
    public Animator anim;
    public string openTriggerName;
    public string closeTriggerName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger(openTriggerName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger(closeTriggerName);
        }
    }
}
