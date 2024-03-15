using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    public GameObject bulletPrefab;
    private float currentTimer;
    public float cooldownTimer = 1f;
    private bool shot;

    private void Update()
    {
        if(!HasStateAuthority) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            shot = true;
            Runner.Spawn(bulletPrefab, transform.position, Quaternion.identity);
        }

        if(shot) currentTimer += Time.deltaTime;

        if(currentTimer >= cooldownTimer)
        {
            shot = false;
            currentTimer = 0;
        }
    }
}
