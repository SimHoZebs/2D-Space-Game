using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInfo : NetworkBehaviour
{
    [Header("Player basic information")]
    [SyncVar (hook = nameof(OnNameChange))] [SerializeField]
    public string playerName;

    [Header("Player UI Information")]

    [SyncVar]
    public int playerHealth;

    [Server]
    private void Start() {
        playerHealth = 100;
    }

    public void OnNameChange(string old, string newName){
        Debug.Log($"Name changed to {newName}!");
    }
}