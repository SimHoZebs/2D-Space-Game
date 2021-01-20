using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Door : NetworkBehaviour
{

    public void OpenDoor(){
        var test = GetComponent<SpriteRenderer>();

        test.color = Color.black;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseDoor(){
        var test = GetComponent<SpriteRenderer>();

        test.color = Color.white;
        GetComponent<BoxCollider2D>().enabled = true;
    }

}
