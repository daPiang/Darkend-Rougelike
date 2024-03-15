using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [Networked, Capacity(4), SerializeField, OnChangedRender(nameof(SyncList))] private NetworkLinkedList<PlayerRef> PlayerRefList => default;
    [Networked, Capacity(4), SerializeField, OnChangedRender(nameof(SyncList))] private NetworkLinkedList<NetworkObject> NetObjectList => default;
    public List<PlayerRef> playerMasterList;
    public List<NetworkObject> objectMasterList;
    private NetworkObject playerNO;
    private NetworkObject localObject;
    private PlayerRef localRef;


    public GameObject mainMenu, gameUi;
    public Button StartGameButton;

    private void Awake() {
        StartGameButton?.onClick.AddListener(() => Rpc_StartGame());
    }

    // private void Update() {
    //     if(mainMenu == null) mainMenu = GameObject.FindWithTag("MainMenu");
    //     if(gameUi == null) gameUi = GameObject.FindWithTag("GUI");
    //     if(StartGameButton == null) StartGameButton = GameObject.FindWithTag("StartGameButton").GetComponent<Button>();
    // }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_AddToDict(PlayerRef playerRef, NetworkObject networkObject)
    {
        PlayerRefList.Add(playerRef);
        // playerMasterList.Add(playerRef);
        // objectMasterList.Add(networkObject);
        NetObjectList.Add(networkObject);

        if(localObject == null) localObject = networkObject;
        if(localRef == null) localRef = playerRef;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_RemoveFromDict(PlayerRef playerRef, NetworkObject networkObject)
    {
        PlayerRefList.Remove(playerRef);
        // playerMasterList.Remove(playerRef);
        // objectMasterList.Remove(networkObject);
        NetObjectList.Remove(networkObject);

        // if(localObject != null) localObject = networkObject;
        // if(localRef != null) localRef = playerRef;
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

    public NetworkObject GetLocalRef()
    {
        return localObject;
    }

    public PlayerRef GetLocalPlayerRef()
    {
        return localRef;
    }

    public void PlayerJoined(PlayerRef player)
    {
        Rpc_AddToDict(player, playerNO);
    }

    public void PlayerLeft(PlayerRef player)
    {
        // throw new System.NotImplementedException();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_StartGame()
    {
        gameUi.SetActive(true);
        mainMenu.SetActive(false);
    }
}
