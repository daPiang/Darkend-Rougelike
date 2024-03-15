using Fusion;
using TMPro;
using UnityEngine;

public class Timer : NetworkBehaviour
{
    public TextMeshProUGUI timerText;
    // public float totalTime = 120f; // Total time in seconds
    [Networked, OnChangedRender(nameof(UpdateTimerText))] private float CurrentTime {get; set;}
    [Networked] private float CurrentMinutes {get; set;}
    [Networked] private float Seconds {get; set;}
    [Networked] public bool Frozen {get; set;}

    private void Start()
    {
        // CurrentTime = totalTime;
    }

    public override void FixedUpdateNetwork()
    {
        if(!HasStateAuthority) return;
        if(!FindObjectOfType<GameManager>().PreGameLobbyBool()) return;
        if(Frozen) return;

        CurrentTime += Runner.DeltaTime;
        UpdateTimerText();
        
    }

    private void UpdateTimerText()
    {
        CurrentMinutes = Mathf.FloorToInt(CurrentTime / 60);
        Seconds = Mathf.FloorToInt(CurrentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", CurrentMinutes, Seconds);
    }

    public float GetMinutes()
    {
        return CurrentMinutes;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_FreezeTimer(bool state)
    {
        Frozen = state;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_ResetTimer()
    {
        Frozen = true;
        CurrentTime = 0;
    }
}
