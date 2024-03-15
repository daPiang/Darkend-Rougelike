using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public PowerUpSelect powerUpScreen;
    [Networked, OnChangedRender(nameof(SyncState))]public bool GameJustStarted {get; set;} = false;
    private bool clientJustStarted;
    [Networked, OnChangedRender(nameof(SyncPreGame))] public bool PreGameLobby {get; set;}
    public bool clientPreGameLobby;
    public Button StartGameButton;

    public delegate void ResetGame();
    public static event ResetGame OnReset;

    private void Awake() {
        StartGameButton?.onClick.AddListener(() => Rpc_StartGameLobby());
    }

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

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_ResetGame()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if(playerObjects.Length > 1)
        {
            if(!playerObjects[0].GetComponent<PlayerLoadout>().IsClient())
            {
                playerObjects[0].GetComponent<PlayerLoadout>().Rpc_ResetLoadout();
            }

            for(int i = 0; i < playerObjects.Length - 1; i++)
            {
                playerObjects[i].transform.position = Vector2.zero;
                playerObjects[i].GetComponent<PlayerHealth>().Rpc_ResetHp();
            }
        }
        else
        {
            playerObjects[0].GetComponent<PlayerLoadout>().Rpc_ResetLoadout();
            playerObjects[0].transform.position = Vector2.zero;
            playerObjects[0].GetComponent<PlayerHealth>().Rpc_ResetHp();
        }
        
        FindObjectOfType<Timer>().Rpc_ResetTimer();
        FindObjectOfType<XPBar>().Rpc_ResetXp();

        PreGameLobby = false;
        GameJustStarted = false;

        FindObjectOfType<EnemyManager>().Rpc_StartSpawning(false);

        OnReset?.Invoke();

        // FindObjectOfType<LobbyManager>().Rpc_ResetGame();
        Rpc_StartGameLobby();
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_EndGame()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if(playerObjects.Length > 1)
        {
            if(!playerObjects[0].GetComponent<PlayerLoadout>().IsClient())
            {
                playerObjects[0].GetComponent<PlayerLoadout>().Rpc_ResetLoadout();
            }

            for(int i = 0; i < playerObjects.Length - 1; i++)
            {
                playerObjects[i].transform.position = Vector2.zero;
                playerObjects[i].GetComponent<PlayerHealth>().Rpc_ResetHp();
            }
        }
        else
        {
            playerObjects[0].GetComponent<PlayerLoadout>().Rpc_ResetLoadout();
            playerObjects[0].transform.position = Vector2.zero;
            playerObjects[0].GetComponent<PlayerHealth>().Rpc_ResetHp();
        }
        
        FindObjectOfType<Timer>().Rpc_ResetTimer();
        FindObjectOfType<XPBar>().Rpc_ResetXp();

        PreGameLobby = false;
        GameJustStarted = false;

        FindObjectOfType<EnemyManager>().Rpc_StartSpawning(false);

        OnReset?.Invoke();

        FindObjectOfType<LobbyManager>().Rpc_ResetGame();
        // return Task.CompletedTask;
    }
}
