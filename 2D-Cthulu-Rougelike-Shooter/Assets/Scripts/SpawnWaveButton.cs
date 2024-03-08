using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class SpawnWaveButton : SimulationBehaviour, IAfterSpawned
{
    public GameObject EnemyPrefab;

    private void Start() {
        // Runner.AddGlobal(this);
    }

    public void SpawnWave()
    {
        Runner.Spawn(EnemyPrefab, new Vector3(8, 3, 0), Quaternion.identity);
        Runner.Spawn(EnemyPrefab, new Vector3(8, 0, 0), Quaternion.identity);
        Runner.Spawn(EnemyPrefab, new Vector3(8, -3, 0), Quaternion.identity);
    }

    public void AfterSpawned()
    {
        // throw new System.NotImplementedException();
        Debug.Log("SPAWNED");
    }
}
