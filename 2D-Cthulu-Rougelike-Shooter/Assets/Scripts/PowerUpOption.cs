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
        if(!HasStateAuthority) return;
        this.gunStats = gunStats;
        SetOptionValues();
    }

    private void SetOptionValues()
    {
        if(!HasStateAuthority) return;
        displayImage.sprite = gunStats.gunSprite;
        displayName.text = gunStats.gunName;
        description.text = gunStats.description;
    }

    public void ChoosePower()
    {
        if(!HasStateAuthority) return;
        // Debug.Log("POWER");
        //Load selected SO into player loadout
        FindObjectOfType<LobbyManager>().GetLocalRef().GetComponent<PlayerLoadout>().guns[
           FindObjectOfType<LobbyManager>().GetLocalRef().GetComponent<PlayerLoadout>().FindNextAvailableSlot() 
        ].AsssignGunStats(gunStats);
        FindObjectOfType<LobbyManager>().GetLocalRef().GetComponent<PlayerLoadout>().guns[
           FindObjectOfType<LobbyManager>().GetLocalRef().GetComponent<PlayerLoadout>().FindNextAvailableSlot() 
        ].gameObject.SetActive(true);
        
        powerScreen.ClosePowerUpSelect();
    }
}
