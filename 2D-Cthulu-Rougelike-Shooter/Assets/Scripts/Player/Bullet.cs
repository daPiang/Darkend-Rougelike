using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour {
    public float lifetime = 1f;
    public float moveSpeed = 2f;
    private bool enemyHit;
    public float damage;

    private void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Enemy"))
        {
            enemyHit = true;
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public override void FixedUpdateNetwork()
    {
        transform.position += new Vector3(moveSpeed * Runner.DeltaTime, 0, 0);

        lifetime -= Runner.DeltaTime;

        if(lifetime < 0 || enemyHit) Runner.Despawn(GetComponent<NetworkObject>());
    }
}