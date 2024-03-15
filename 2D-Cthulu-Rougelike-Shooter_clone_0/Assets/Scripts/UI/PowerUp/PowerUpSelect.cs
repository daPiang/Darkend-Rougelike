using System.Linq;
using Fusion;
using UnityEngine;

public class PowerUpSelect : NetworkBehaviour
{
    public GunSO[] lootPool;
    public PowerUpOption[] options;
    public GameObject powerUpScreen;

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcOpenPowerUpSelect()
    {
        FindObjectOfType<GameManager>().DisableOnGameStartBool();
        //Freeze Monsters
        //Freeze Time
        FindObjectOfType<Timer>().Rpc_FreezeTimer(true);
        FindObjectOfType<EnemyManager>().Rpc_StartSpawning(false);
        // EnemyManager.Rpc_StartSpawning(Runner, false);
        //Freeze Player

        powerUpScreen.SetActive(true);
        SelectGunsToDisplay();
    }
    // [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void ClosePowerUpSelect()
    {
        // Debug.Log("CLOSING");

        FindObjectOfType<Timer>().Rpc_FreezeTimer(false);
        FindObjectOfType<EnemyManager>().Rpc_StartSpawning(true);
        // EnemyManager.Rpc_StartSpawning(true);

        powerUpScreen.SetActive(false);
        // SelectGunsToDisplay();
    }

    private void SelectGunsToDisplay()
    {
        Shuffle(lootPool);
        
        // Select the first three elements
        GunSO gun1 = lootPool[0];
        GunSO gun2 = lootPool[1];
        GunSO gun3 = lootPool[2];

        // Debug.Log($"Option 1: {gun1.gunName}");
        // Debug.Log($"Option 2: {gun2.gunName}");
        // Debug.Log($"Option 3: {gun3.gunName}");

        LoadGunStats(gun1, 0);
        LoadGunStats(gun2, 1);
        LoadGunStats(gun3, 2);
    }

    private void LoadGunStats(GunSO gunStats, int x)
    {
        options[x].PassGunStats(gunStats);
    }

    // Shuffle array using Fisher-Yates algorithm
    private void Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
