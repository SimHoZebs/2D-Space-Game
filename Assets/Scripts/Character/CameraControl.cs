using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class CameraControl : NetworkBehaviour
{
    //Code for controlling the top down view camera's motion.
    private CinemachineVirtualCamera freeCam;

    [Client]
    private void Start() {
        if (!isLocalPlayer){ return;}
        freeCam = GameObject.FindGameObjectWithTag("Vcam").GetComponent<CinemachineVirtualCamera>();

        freeCam.Follow = transform;
        freeCam.LookAt = transform;
    }
}