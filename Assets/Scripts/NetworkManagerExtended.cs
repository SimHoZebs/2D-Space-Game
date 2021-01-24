using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerExtended : NetworkManager
{

    [SerializeField] private bool debugMode = false;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject defaultWeapon;
    private string typedName;

    public override void Start()
    {
        base.Start();

        if (debugMode){
            typedName = "DebugMode";
            StartHost();
        }
    }

    //This is constantly updated at every change via Input Field in Unity.
    //It could be little more efficient to update it when the button is pressed instead.
    public void SetPlayerName(string _){
        typedName = _;
    }
    public void SetNetworkAddress(string _){
        networkAddress = _;
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        player.GetComponent<PlayerInfo>().playerName = typedName;

        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
