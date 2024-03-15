using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public PowerUpSelect powerUpScreen;
    [Networked, OnChangedRender(nameof(SyncState))]public bool GameJustStarted {get; set;} = false;
    private bool clientJustStarted;
    [Networked, OnChangedRender(nameof(SyncPreGame))] public bool PreGameLobby {get; set;}
    private bool clientPreGameLobby;
    public Button StartGameButton;

    private void Awake() {
        StartGameButton?.onClick.AddListener(() => Rpc_StartGameLobby());
    }

    // private void Update() {
    //     if(powerUpScreen == null) powerUpScreen = GameObject.FindGameObjectWithTag("PowerUpScreen").GetComponent<PowerUpSelect>();
    //     if(StartGameButton == null) StartGameButton = GameObject.FindWithTag("StartGameButton").GetComponent<Button>();
    // }

    public override void FixedUpdateNetwork() {
        if(!PreGameLobby) return;

        if(HasStateAuthority)
        {
            if(clientJustStarted) 
            {
                powerUpScreen.RpcOpenPowerUpSelect();
            }
        }
        else
        {
            if(clientJustStarted) 
            {
                powerUpScreen.RpcOpenPowerUpSelect();
            }
        }
    }

    private void SyncState()
    {
        // if(GameJustStarted) 
        // {
        //     powerUpScreen.RpcOpenPowerUpSelect();
        // }
        clientJustStarted = GameJustStarted;
    }

    private void SyncPreGame()
    {
        clientPreGameLobby = PreGameLobby;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_StartGameLobby()
    {
        PreGameLobby = true;
        GameJustStarted = true;
    }

    public bool PreGameLobbyBool()
    {
        return PreGameLobby;
    }

    public void DisableOnGameStartBool()
    {
        GameJustStarted = false;
    }
}
