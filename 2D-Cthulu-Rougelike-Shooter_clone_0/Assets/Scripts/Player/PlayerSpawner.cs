using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public LobbyManager lobbyMgr;

    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            // Debug.Log(Runner.gameObject);
            NetworkObject playerObject = Runner.Spawn(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            playerObject.gameObject.name = "Host Player";
            Runner.SetPlayerObject(player, playerObject);

            lobbyMgr.SetNO(playerObject);
        }
    }
}
