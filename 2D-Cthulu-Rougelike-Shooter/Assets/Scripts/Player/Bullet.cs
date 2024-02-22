using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour {
    public float lifetime = 1f;
    public float moveSpeed = 2f;

    public override void FixedUpdateNetwork()
    {
        transform.position += new Vector3(moveSpeed * Runner.DeltaTime, 0, 0);

        lifetime -= Runner.DeltaTime;

        if(lifetime < 0) Runner.Despawn(GetComponent<NetworkObject>());
    }
}