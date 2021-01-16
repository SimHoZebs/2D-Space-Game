using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovementControl : NetworkBehaviour
{
    //Controls player motion and rotation.

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float walkSpeed = 5f;

    [Client]
    void Update() {
        if (!isLocalPlayer){ return;}
        Move();
    }

    [Client]
    private void Move(){
        var newPosition = new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime,Input.GetAxis("Vertical")*Time.deltaTime);
        transform.position += newPosition*(moveSpeed - walkSpeed*Input.GetAxis("Walk"));
    }

}
