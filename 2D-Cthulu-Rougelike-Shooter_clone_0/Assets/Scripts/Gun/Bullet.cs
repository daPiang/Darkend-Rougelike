using UnityEngine;
using Fusion;

public class Bullet : NetworkBehaviour {
    public float lifetime = 1f;
    public float moveSpeed = 2f;
    private bool enemyHit;
    private float damage;
    private bool canPierce;
    private float pierceCount;
    public Vector3 direction;

    private void OnEnable() {
        GameManager.OnReset += DespawnSelf;
    }

    private void OnDisable() {
        GameManager.OnReset -= DespawnSelf;
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

        if(canPierce)
        {
            if(lifetime < 0 || pierceCount <= 0) Runner.Despawn(GetComponent<NetworkObject>());
        }
        else
        {
            if(lifetime < 0 || enemyHit) Runner.Despawn(GetComponent<NetworkObject>());
        }
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public void SetDamage(float damage)
    {   
        this.damage = damage;
    }

    public void SetSpeed(float speed)
    {   
        this.moveSpeed = speed;
    }

    public void SetPierce(bool state)
    {
        this.canPierce = state;
    }

    public void SetPierceCount(int count)
    {
        this.pierceCount = count;
    }

    public void SetSize(float size)
    {
        transform.localScale = new(size, size, size);
    }

    private void DespawnSelf()
    {
        Runner.Despawn(GetComponent<NetworkObject>());
    }
}