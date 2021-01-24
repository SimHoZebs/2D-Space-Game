using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovementControl : NetworkBehaviour
{
    //Controls player motion and rotation.
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float walkSlowdown = 3f;

    [Client]
    private void Start() {
        if (!isLocalPlayer){ return;}

        rb = GetComponent<Rigidbody2D>();

    }

    [Client]
    void Update() {
        if (!isLocalPlayer){ return;}
        FaceMouse();
    }

    [Client]
    private void FixedUpdate() {
        if (!isLocalPlayer){ return;}

        Move();
    }

    [Client]
    private void Move(){

        var newPosition = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        rb.velocity = newPosition*(moveSpeed - walkSlowdown*Input.GetAxis("Walk"));
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
