using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class LobbyManager : NetworkPersistentSingleton<LobbyManager>
{
    // [Networked, Capacity(4), SerializeField] private NetworkLinkedList<PlayerRef> PlayerRefList => default;
    // [Networked, Capacity(4), SerializeField] private NetworkLinkedList<NetworkObject> NetObjectList => default;
    public List<PlayerRef> playerMasterList;
    public List<NetworkObject> objectMasterList;


    // [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_AddToDict(PlayerRef playerRef, NetworkObject networkObject)
    {
        // PlayerRefList.Add(playerRef);
        playerMasterList.Add(playerRef);
        objectMasterList.Add(networkObject);
        // NetObjectList.Add(networkObject);
    }

    // [Rpc(RpcSources.All, RpcTargets.All)]
    private void Rpc_RemoveFromDict(PlayerRef playerRef, NetworkObject networkObject)
    {
        // PlayerRefList.Remove(playerRef);
        playerMasterList.Remove(playerRef);
        objectMasterList.Remove(networkObject);
        // NetObjectList.Remove(networkObject);
    }

    public static void AddPlayerToList(PlayerRef playerRef, NetworkObject networkObject)
    {
        instance.Rpc_AddToDict(playerRef, networkObject);
    }

    public static void RemovePlayerFromList(PlayerRef playerRef, NetworkObject networkObject)
    {
        instance.Rpc_RemoveFromDict(playerRef, networkObject);
    }

    // public NetworkLinkedList<PlayerRef> GetPlayerList()
    // {
    //     return PlayerRefList;
    // }

    // public NetworkLinkedList<NetworkObject> GetObjectList()
    // {
    //     return NetObjectList;
    // }

    public List<PlayerRef> GetPlayerMasterList()
    {
        return playerMasterList;
    }

    public List<NetworkObject> GetObjectMasterList()
    {
        return objectMasterList;
    }
}
