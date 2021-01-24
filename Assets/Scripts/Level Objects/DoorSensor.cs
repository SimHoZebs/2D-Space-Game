using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DoorSensor : MonoBehaviour
{
    [SerializeField] GameObject doorObj;
    private Door door;

    private void Start() {
        door = doorObj.GetComponent<Door>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Structure")){ return;}

        door.OpenDoor();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Structure")){ return;}

        door.CloseDoor();
    }
}
