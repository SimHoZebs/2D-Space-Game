using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DefaultGun : Weapon
{
    [SerializeField] [Range(0,1000)]
    private int maxAmmoCount = 100;

    [SerializeField] private int currAmmoCount;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 1500f;
    [SerializeField] private Transform bulletSpawnPoint;

    private Transform playerTransform;

    protected override void StartInternal(){
        playerTransform = GetComponentInParent<Transform>();
        currAmmoCount = maxAmmoCount;
    }

    protected override void PressUseItemInternal()
    {
        if (currAmmoCount == 0){ return;}
        currAmmoCount--;

        var shotBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        shotBullet.GetComponent<Rigidbody2D>().velocity = playerTransform.right * bulletSpeed;
    }
}
