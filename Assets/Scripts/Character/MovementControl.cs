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
        FaceMouse();
    }

    [Client]
    private void Move(){
        var newPosition = new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime,Input.GetAxis("Vertical")*Time.deltaTime);
        transform.position += newPosition*(moveSpeed - walkSpeed*Input.GetAxis("Walk"));
    }

    [Client]
    private void FaceMouse(){
        //Shortcut
        Vector3 pos = transform.position;

        //Convert mouse coords on screen to on world
        var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //By subtracting the mosue coords with player's coords, each coord is the distance from the player.
        var relativeX = cursorPos.x - pos.x;
        var relativeY = cursorPos.y - pos.y;

        //Change player's relative Z axis according to this Vector
        transform.up = new Vector2(relativeX, relativeY);
    }
}
