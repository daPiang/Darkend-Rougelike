using Fusion;
using TMPro;
using UnityEngine;

public class Timer : NetworkBehaviour
{
    public TextMeshProUGUI timerText;
    // public float totalTime = 120f; // Total time in seconds
    [Networked] private float CurrentTime {get; set;}
    [Networked] private float CurrentMinutes {get; set;}
    [Networked] public bool Frozen {get; set;}

    private void Start()
    {
        // CurrentTime = totalTime;
    }

    public override void FixedUpdateNetwork()
    {
        if(!HasStateAuthority) return;
        if(Frozen) return;

        CurrentTime += Runner.DeltaTime;
        UpdateTimerText();
        
    }

    private void UpdateTimerText()
    {
        CurrentMinutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", CurrentMinutes, seconds);
    }

    public float GetMinutes()
    {
        return CurrentMinutes;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_FreezeTimer(bool state)
    {
        Frozen = state;
    }
}
