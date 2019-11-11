using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public float range = 100f;
    public WeaponController weaponController;
    public float damageRate = 50;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float sqrDistance = (transform.position - initialPosition).sqrMagnitude;
        if (sqrDistance > range * range)
        {
            //Destroy(gameObject);
            PutIntoPool();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
        HarmTarget(collision.gameObject);
        PutIntoPool();
    }

    private void PutIntoPool()
    {
        List<GameObject> pool = weaponController.GetBulletPool();
        pool.Add(gameObject);
        gameObject.SetActive(false);
    }

    private void HarmTarget(GameObject aTargetObj)
    {
        if (aTargetObj.GetComponent<HealthManager>())
            aTargetObj.GetComponent<HealthManager>().ModifyHealth(-damageRate);
    }
}
