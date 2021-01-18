using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shoot : NetworkBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject gun;
    [SerializeField] private float bulletSpeed = 1500f;

    [Client]
    private void Start() {
        if (!isLocalPlayer){ return;}
    }

    [Client]
    void Update()
    {
        if (!isLocalPlayer){ return;}

        if (Input.GetButtonDown("Fire")){
            Debug.Log("Input Fire pressed");
            FireGun();
        }
    }

    private void FireGun(){
        var test = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        test.GetComponent<Rigidbody2D>().velocity = transform.up* bulletSpeed;
    }
}
