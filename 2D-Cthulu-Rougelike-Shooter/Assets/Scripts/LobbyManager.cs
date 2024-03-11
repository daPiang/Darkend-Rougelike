using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class LobbyManager : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [Networked, Capacity(4), SerializeField, OnChangedRender(nameof(SyncList))] private NetworkLinkedList<PlayerRef> PlayerRefList => default;
    [Networked, Capacity(4), SerializeField, OnChangedRender(nameof(SyncList))] private NetworkLinkedList<NetworkObject> NetObjectList => default;
    public List<PlayerRef> playerMasterList;
    public List<NetworkObject> objectMasterList;
    private NetworkObject playerNO;

    private void Start() {
        
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_AddToDict(PlayerRef playerRef, NetworkObject networkObject)
    {
        PlayerRefList.Add(playerRef);
        // playerMasterList.Add(playerRef);
        // objectMasterList.Add(networkObject);
        NetObjectList.Add(networkObject);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_RemoveFromDict(PlayerRef playerRef, NetworkObject networkObject)
    {
        PlayerRefList.Remove(playerRef);
        // playerMasterList.Remove(playerRef);
        // objectMasterList.Remove(networkObject);
        NetObjectList.Remove(networkObject);
    }

    // [Rpc(RpcSources.All, RpcTargets.All)]
    public void SyncList()
    {
        if(playerMasterList.Count != PlayerRefList.Count)
        {
            playerMasterList.Clear();
            foreach(var x in PlayerRefList)
            {
                playerMasterList.Add(x);
            }
        }
        
        if(objectMasterList.Count != NetObjectList.Count)
        {
            objectMasterList.Clear();
            foreach(var x in NetObjectList)
            {
                objectMasterList.Add(x);
            }
        }
    }

    public NetworkLinkedList<PlayerRef> GetPlayerMasterList()
    {
        return PlayerRefList;
    }

    public NetworkLinkedList<NetworkObject> GetObjectMasterList()
    {
        return NetObjectList;
    }

    public void SetNO(NetworkObject networkObject)
    {
        playerNO = networkObject;
    }

    public void PlayerJoined(PlayerRef player)
    {
        Rpc_AddToDict(player, playerNO);
    }

    public void PlayerLeft(PlayerRef player)
    {
        // throw new System.NotImplementedException();
    }
}
