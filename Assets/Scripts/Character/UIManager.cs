using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UIManager : NetworkBehaviour
{
    [SerializeField] private Canvas menuScreen;

    [Client]
    private void Start() {
        if (!isLocalPlayer){ 
            Debug.Log("UIManager is not with player");
            return;
        }


        menuScreen = GameObject.FindGameObjectWithTag("Menu").GetComponent<Canvas>();

        menuScreen.enabled = false;
    }

    [Client]
    void Update()
    {
        if (!isLocalPlayer){ return;}
        EnterMenu();

    }

    private void EnterMenu(){
        if (Input.GetButtonDown("Cancel")){

            menuScreen.enabled = menuScreen.enabled? false:true;
        }
    }
}
