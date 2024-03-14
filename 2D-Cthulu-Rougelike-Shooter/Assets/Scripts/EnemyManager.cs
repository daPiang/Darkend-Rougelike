using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class EnemyManager : SimulationBehaviour
{
    public GameObject[] enemyPrefabs;
    public Vector2 spawnArea;
    public float spawnTimer;
    private float timer;
    public bool gameStarted;
    public LobbyManager lobbyMgr;
    public Timer gameTime;

    // private void Start() {
    //     // Runner.AddGlobal(FindAnyObjectByType<GunBase>());
    // }

    private void Update() {
        // Debug.Log(Runner.gameObject);
        timer -= Time.deltaTime;
        if(timer < 0 && gameStarted)
        {
            // Debug.Log(Runner.gameObject);
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        position += lobbyMgr.GetObjectMasterList()[0].transform.position;

        NetworkObject new_enemy = Runner.Spawn(
            enemyPrefabs[SpawnWeights(
                FindFirstObjectByType<Timer>().GetMinutes()
            )], 
            position, 
            Quaternion.identity
            );
        // newEnemy.transform.position = position;
        new_enemy.GetComponent<Enemy>().SetTarget(lobbyMgr.GetObjectMasterList()[0].gameObject);
    }

    private int SpawnWeights(float minutes)
    {
        if(minutes >= 0 && minutes <= 2)
        {
            spawnTimer = 4;
            return 0;
        }

        if(minutes >= 2 && minutes <= 4)
        {
            spawnTimer = 2.5f;
            return Random.Range(0,2);
        }

        if(minutes >= 4 && minutes <= 6)
        {
            spawnTimer = 2;
            return Random.Range(0,3);
        }

        if(minutes >= 6 && minutes <= 8)
        {
            spawnTimer = 1.8f;
            return Random.Range(0,4);
        }

        if(minutes >= 8 && minutes <= 10)
        {
            spawnTimer = 1.5f;
            return Random.Range(0,5);
        }

        if(minutes >= 10)
        {
            spawnTimer = 1;
            return Random.Range(2,5);
        }

        return 0;
    }

    private Vector3 GenerateRandomPosition()
    {
        // return new(UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
        // UnityEngine.Random.Range(-spawnArea.y, spawnArea.y),
        // 0f);
        Vector3 position = new();

        float f = Random.value > 0.5f ? -1f : 1f;
        if(Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else {
            position.y = Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }

        position.z = 0;

        return position;
    }
}
