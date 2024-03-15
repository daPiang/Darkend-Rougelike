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
    public GameObject NetworkMgr, SimMgr;

    private void Awake()
    {
        HostButton?.onClick.AddListener(() => ConnectToRunner(GameMode.Single));
        JoinButton?.onClick.AddListener(() => ConnectToRunner(GameMode.Single));
        
        // HostMenuBackButton?.onClick.AddListener(() => DisconnectFromRunner());
        // HostMenuBackButton?.onClick.AddListener(() => ConnectToRunner(GameMode.Single));
    }

    // private void Update() {
    //     if(HostButton == null) HostButton = GameObject.FindGameObjectWithTag("HostButton").GetComponent<Button>();
    //     if(JoinButton == null) JoinButton = GameObject.FindGameObjectWithTag("JoinButton").GetComponent<Button>();
    //     if(HostMenuBackButton == null) HostMenuBackButton = GameObject.FindGameObjectWithTag("HostMenuBackButton").GetComponent<Button>();
    //     if(lobby == null) lobby = FindObjectOfType<LobbyManager>();
    // }

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

    // public async void DisconnectFromRunner()
    // {
    //     // if(Runner != null && Runner.IsRunning) await Runner.Shutdown(destroyGameObject: false);
    //     // Instantiate(NetworkMgr);
    //     // Instantiate(SimMgr);
    // }
}
