using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    public Camera cam;

    private void Update() {
        if(!HasStateAuthority) return;

        // if(!cam.gameObject.activeSelf) cam.gameObject.SetActive(true);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = FindObjectOfType<LobbyManager>().GetLocalRef().transform;
    }
}
