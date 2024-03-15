using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SimulationManager : SimulationBehaviour
{
    public Button HostButton, JoinButton, HostMenuBackButton;
    private NetworkRunner _runner;

    public LobbyManager lobby;
    // public GameObject NetworkMgr, SimMgr;
    
    private void Awake()
    {
        HostButton?.onClick.AddListener(() => ConnectToRunner(GameMode.Shared));
        JoinButton?.onClick.AddListener(() => ConnectToRunner(GameMode.Shared)); 
    }

    private void Update() {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(Runner.IsRunning) Runner.Shutdown();
        }
    }


    public async void ConnectToRunner(GameMode mode)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
       // _runner.ProvideInput = true;
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
          //  SessionName = UI.RoomName,
            PlayerCount = 2,
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }
}
