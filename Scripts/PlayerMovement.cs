using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forceMagnitude = 10f;
    public GameObject weaponDummy;
    float horizontal;
    float vertical;
    public bool isDead;
    //bool onGround;

    private void Update()
    {
        if(!isDead)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            transform.position += transform.forward * vertical * forceMagnitude;
            transform.position += transform.right * horizontal * forceMagnitude;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            //transform.rotation = Quaternion.Euler(
            //    transform.rotation.eulerAngles.x,
            //    transform.rotation.eulerAngles.y + mouseX,
            //    transform.rotation.eulerAngles.z
            //);

            transform.rotation *= Quaternion.Euler(0, mouseX, 0);
            Camera.main.transform.rotation *= Quaternion.Euler(-mouseY, 0, 0);
            if (weaponDummy != null)
            {
                weaponDummy.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);

            }

        }
        
    }

    //private void FixedUpdate()
    //{
    //    if (onGround)
    //    {
    //        GetComponent<Rigidbody>().AddForce(transform.forward * vertical * forceMagnitude, ForceMode.VelocityChange);
    //        GetComponent<Rigidbody>().AddForce(transform.right * horizontal * forceMagnitude, ForceMode.VelocityChange);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    onGround = true;
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    onGround = false;
    //}

}
