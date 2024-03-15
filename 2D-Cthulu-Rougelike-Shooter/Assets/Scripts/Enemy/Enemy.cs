using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    public float moveSpeed = 1;
    public float damage = 1;
    public float xp = 1;
    public Rigidbody2D rb;

    private GameObject target;
    [Networked, OnChangedRender(nameof(SyncHp))] public float Hp {get; set;} = 1;
    public float localHp = 1;
    private bool dead;
    private bool tookDamage;

    private void OnEnable() {
        GameManager.OnReset += DespawnSelf;
    }

    private void OnDisable() {
        GameManager.OnReset -= DespawnSelf;
    }
    private void Update() {
        if(HasStateAuthority)
        {
            if(localHp <= 0)
            {
                GameObject.FindGameObjectWithTag("GUI").GetComponent<XPBar>().RpcAddXp(xp);
                dead = true;
            }
        }
    }

    public override void Render()
    {
        if(tookDamage)
        {
            GetComponent<SpriteFlasher>().FlashRed();
            tookDamage = false;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if(HasStateAuthority)
        {    
            if(target != null)
            {
                //MoveEnemyTowardsPoint
                Vector3 targetPosition = target.transform.position;
                Vector3 moveDirection = (targetPosition - transform.position).normalized;

                if(moveDirection.x > 0) transform.localScale = new(1,1,1);
                if(moveDirection.x < 0) transform.localScale = new(-1,1,1);

                if(!FindAnyObjectByType<Timer>().Frozen) transform.Translate(moveDirection * moveSpeed * Runner.DeltaTime);
            }

            if(dead) Runner.Despawn(GetComponent<NetworkObject>());
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void SyncHp()
    {
        localHp = Hp;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcTakeDamage(float damage)
    {
        Hp -= damage;
        tookDamage = true;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.transform.CompareTag("Player"))
        {
            // Debug.Log("DAMAGING PLAYER");
            other.gameObject.GetComponent<PlayerHealth>().Rpc_TakeDamage(damage);
        }
    }
    
    private void DespawnSelf()
    {
        Runner.Despawn(GetComponent<NetworkObject>());
    }
}
