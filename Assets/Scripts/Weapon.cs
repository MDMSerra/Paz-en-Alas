using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Transform _firePoint;
    public void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }
    public void Shoot()
    {
        
        if (bulletPrefab != null && _firePoint != null)
        {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, this.transform.rotation) as GameObject;
        }
    }
}
