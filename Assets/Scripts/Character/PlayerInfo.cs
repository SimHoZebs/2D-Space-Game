using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInfo : NetworkBehaviour
{
    [SyncVar (hook = nameof(OnNameChange))] [SerializeField]
    public string playerName;

    public void OnNameChange(string old, string newName){
        Debug.Log($"Name changed to {newName}!");
    }
}