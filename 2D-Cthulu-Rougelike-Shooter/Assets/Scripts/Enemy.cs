using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Enemy : NetworkBehaviour
{
    public float moveSpeed = 1;
    public Rigidbody2D rb;

    private GameObject target;
    public float hp = 1;
    private bool dead;

    private void Update() {
        if(HasStateAuthority)
        {
            if(hp <= 0)
            {
                dead = true;
            }
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
                transform.Translate(moveDirection * moveSpeed * Runner.DeltaTime);
                // rb.velocity = new Vector3(moveDirection.x, moveDirection.y) * moveSpeed;
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
    }
}
