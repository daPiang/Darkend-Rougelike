using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpOption : NetworkBehaviour
{
    public GunSO gunStats;
    public Image displayImage;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI description;
    public PowerUpSelect powerScreen;
    
    private void Update() {
        //if I click on any element of this object. do something
    }

    public void PassGunStats(GunSO gunStats)
    {
        this.gunStats = gunStats;
        SetOptionValues();
    }

    private void SetOptionValues()
    {
        displayImage.sprite = gunStats.gunSprite;
        displayName.text = gunStats.gunName;
        description.text = gunStats.description;
    }

    public void ChoosePower()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        
        int playerIndex;
        if (!playerObjects[0].GetComponent<PlayerLoadout>().IsClient())
        {
            // Runner.FindObject();
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
        }

        playerObjects[playerIndex].GetComponent<PlayerLoadout>().guns[
        playerObjects[playerIndex].GetComponent<PlayerLoadout>().FindNextAvailableSlot() 
        ].Rpc_AsssignGunStats(gunStats.index);
        
        powerScreen.ClosePowerUpSelect();
    }
}
