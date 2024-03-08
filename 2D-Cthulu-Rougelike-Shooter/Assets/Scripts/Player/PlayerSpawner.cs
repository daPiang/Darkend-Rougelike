using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            NetworkObject playerObject = Runner.Spawn(PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            LobbyManager.AddPlayerToList(player, playerObject);

            Runner.Spawn(EnemyPrefab, new Vector3(8, 3, 0), Quaternion.identity);
            Runner.Spawn(EnemyPrefab, new Vector3(8, 0, 0), Quaternion.identity);
            Runner.Spawn(EnemyPrefab, new Vector3(8, -3, 0), Quaternion.identity);
        }
    }
}
