using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public PowerUpSelect powerUpScreen;
    public bool gameJustStarted = true;

    private void Update() {
        if(!HasStateAuthority) return;

        if(gameJustStarted) 
        {
            powerUpScreen.OpenPowerUpSelect();
        }
    }

    public void DisableOnGameStartBool()
    {
        gameJustStarted = false;
    }
}
