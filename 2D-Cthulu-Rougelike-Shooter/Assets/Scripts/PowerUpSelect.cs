using System.Linq;
using Fusion;
using UnityEngine;

public class PowerUpSelect : NetworkBehaviour
{
    public GunSO[] lootPool;
    public PowerUpOption[] options;
    public GameObject powerUpScreen;

    public void OpenPowerUpSelect()
    {
        if(!HasStateAuthority) return;

        FindObjectOfType<GameManager>().DisableOnGameStartBool();
        //Freeze Monsters
        //Freeze Time
        FindObjectOfType<Timer>().Rpc_FreezeTimer(true);
        //Freeze Player

        powerUpScreen.SetActive(true);
        SelectGunsToDisplay();
    }
    
    public void ClosePowerUpSelect()
    {
        if(!HasStateAuthority) return;
        Debug.Log("CLOSING");

        FindObjectOfType<Timer>().Rpc_FreezeTimer(false);

        powerUpScreen.SetActive(false);
        // SelectGunsToDisplay();
    }

    private void SelectGunsToDisplay()
    {
        if(!HasStateAuthority) return;
        Shuffle(lootPool);
        
        // Select the first three elements
        GunSO gun1 = lootPool[0];
        GunSO gun2 = lootPool[1];
        GunSO gun3 = lootPool[2];

        LoadGunStats(gun1, 0);
        LoadGunStats(gun2, 1);
        LoadGunStats(gun3, 2);
    }

    private void LoadGunStats(GunSO gunStats, int x)
    {
        if(!HasStateAuthority) return;
        options[x].PassGunStats(gunStats);
    }

    // Shuffle array using Fisher-Yates algorithm
    private void Shuffle<T>(T[] array)
    {
        if(!HasStateAuthority) return;
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
