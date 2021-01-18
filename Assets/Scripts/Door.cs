using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Door : NetworkBehaviour
{
    [SerializeField] private float doorOpenSpeed = 2f;

    void Start(){
        
    }

    void Update()
    {
        
    }

    public void OpenDoor(){
        var test = GetComponent<SpriteRenderer>().color;

        test.a = Mathf.Lerp(255, 15, doorOpenSpeed);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseDoor(){
        var test = GetComponent<SpriteRenderer>().color;

        test.a = Mathf.Lerp(15, 255, doorOpenSpeed);
        GetComponent<BoxCollider2D>().enabled = true;
    }

}
