using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    public float moveSpeed = 1;

    public override void FixedUpdateNetwork()
    {
        if(LobbyManager.Instance.GetPlayerMasterList().Count > 0)
        {
            //MoveEnemyTowardsPoint
            Vector3 targetPosition = LobbyManager.Instance.GetObjectMasterList()[0].transform.position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.Translate(moveDirection * moveSpeed * Runner.DeltaTime);
        }
    }
}
