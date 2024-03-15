using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    public override void FixedUpdateNetwork() {
        if(!HasStateAuthority) return;
        if(FindObjectOfType<CinemachineVirtualCamera>().Follow) return;

        // if(!cam.gameObject.activeSelf) cam.gameObject.SetActive(true);
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        
        int playerIndex;
        if (!playerObjects[0].GetComponent<PlayerLoadout>().IsClient())
        {
            // Runner.FindObject();
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
        }

        FindObjectOfType<CinemachineVirtualCamera>().Follow = playerObjects[playerIndex].transform;
    }
}
