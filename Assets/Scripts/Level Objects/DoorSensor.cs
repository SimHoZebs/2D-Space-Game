using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DoorSensor : NetworkBehaviour
{
    [SerializeField] GameObject doorObj;
    private Door door;

    [Server]
    private void Start() {
        door = doorObj.GetComponent<Door>();
    }

    [Server]
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Structure")){ return;}

        door.OpenDoor();
    }

    [Server]
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Structure")){ return;}

        door.CloseDoor();
    }
}
