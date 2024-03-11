using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class EnemyManager : SimulationBehaviour
{
    public GameObject enemyPrefab;
    public Vector2 spawnArea;
    public float spawnTimer;
    private float timer;
    public bool gameStarted;
    public LobbyManager lobbyMgr;

    private void Update() {
        // Debug.Log(Runner.gameObject);
        timer -= Time.deltaTime;
        if(timer < 0 && gameStarted)
        {
            Debug.Log(Runner.gameObject);
            SpawnEnemy();
            timer = spawnTimer;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        position += lobbyMgr.GetObjectMasterList()[0].transform.position;

        NetworkObject new_enemy = Runner.Spawn(enemyPrefab, position, Quaternion.identity);
        // newEnemy.transform.position = position;
        new_enemy.GetComponent<Enemy>().SetTarget(lobbyMgr.GetObjectMasterList()[0].gameObject);
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
