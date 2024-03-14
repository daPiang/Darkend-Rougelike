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
    public float hp = 1;
    private bool dead;
    private bool tookDamage;

    private void Update() {
        if(HasStateAuthority)
        {
            if(hp <= 0)
            {
                GameObject.FindGameObjectWithTag("GUI").GetComponent<XPBar>().AddXp(xp);
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
                // rb.velocity = new Vector3(moveDirection.x, moveDirection.y) * moveSpeed * Runner.DeltaTime;
                // rb.MovePosition(targetPosition * moveSpeed * Runner.DeltaTime);
                // rb.AddForce(moveDirection * moveSpeed * Runner.DeltaTime);
            }

            if(dead) Runner.Despawn(GetComponent<NetworkObject>());
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        tookDamage = true;
    }
}
