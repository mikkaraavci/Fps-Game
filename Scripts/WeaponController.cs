using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bulletSpawnPoint;
    public float bulletSpeed = 4f;
    public float coolDownDuration = 1.2f;
    public bool inUse;
    public ParticleSystem barrelBlowEffect;
    private bool canFire = true;
    public bool onPlayer;

    private List<GameObject> bulletPool = new List<GameObject>();
    

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && inUse && canFire && onPlayer)
        {
            Fire();
        }
    }

    //public void ReleaseFire()
    //{
    //    canFire = true;
    //}

    public void Fire()
    {
        if (inUse && canFire)
        {
            canFire = false;
            GameObject bullet = null;
            if (bulletPool.Count == 0)
            {
                GameObject bulletPrefab = Resources.Load("Bullet") as GameObject;
                bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform);
            }
            else
            {
                bullet = bulletPool[0];
                bulletPool.RemoveAt(0);
                bullet.transform.SetParent(bulletSpawnPoint.transform);
                bullet.SetActive(true);
            }
            bullet.GetComponent<BulletDestroyer>().weaponController = GetComponent<WeaponController>();
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;
            bullet.transform.SetParent(null);
            Vector3 vel = bullet.transform.forward * bulletSpeed;
            bullet.GetComponent<Rigidbody>().velocity = vel;
            //Invoke("ReleaseFire", coolDownDuration);
            StartCoroutine(ReleaseFire());

            barrelBlowEffect.Play();
        }
    }

    private IEnumerator ReleaseFire()
    {
        yield return new WaitForSeconds(coolDownDuration);
        canFire = true;
    }

    public List<GameObject> GetBulletPool()
    {
        return bulletPool;
    }

}
