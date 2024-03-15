using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour {
    public float lifetime = 1f;
    public float moveSpeed = 2f;
    private bool enemyHit;
    private float damage;
    public Vector3 direction;

    private void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Enemy"))
        {
            enemyHit = true;
            other.gameObject.GetComponent<Enemy>().RpcTakeDamage(damage);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if(FindObjectOfType<Timer>().Frozen) return;

        transform.position += moveSpeed * Runner.DeltaTime * direction;

        lifetime -= Runner.DeltaTime;

        if(lifetime < 0 || enemyHit) Runner.Despawn(GetComponent<NetworkObject>());
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public void SetDamage(float damage)
    {   
        this.damage = damage;
    }
}