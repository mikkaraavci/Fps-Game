using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public WeaponController weaponController;
    public float range = 10f;
    private GameObject player;
    private float sqrDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(!player.GetComponent<PlayerMovement>().isDead)
        {
            sqrDistance = (player.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < range * range)
            {
                Quaternion currentRotation = transform.rotation;
                Vector3 direction = player.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction, player.transform.up);
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 0.3f);
                Fire();
            }
        }
        
    }

    public void Fire()
    {
        weaponController.Fire();
    }

}
