using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    // public float moveSpeed = 2f;
    public PlayerStatsSO stats;

    public override void FixedUpdateNetwork()
    {
        if(!HasStateAuthority) return;

        Vector3 move = new(
            Input.GetAxis("Horizontal") * Runner.DeltaTime * stats.moveSpeed,
            Input.GetAxis("Vertical") * Runner.DeltaTime * stats.moveSpeed,
            0
        );

        if(move != Vector3.zero) gameObject.transform.position += move;
    }
}
