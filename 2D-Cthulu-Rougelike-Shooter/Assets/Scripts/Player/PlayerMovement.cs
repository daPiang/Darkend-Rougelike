using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    // public float moveSpeed = 2f;
    public PlayerStatsSO stats;
    public Animator anim;
    private Vector3 move;
    private Vector3 previousPosition;
    public SpriteRenderer sprite;

    private void Start()
    {
        // Initialize previous position
        previousPosition = transform.position;
    }

    public override void Render()
    {
        if(FindObjectOfType<Timer>().Frozen) return;

        if(HasStateAuthority)
        {
            if(Input.GetKey(KeyCode.A))
            {
                //make localscale x negative
                transform.localScale = new Vector3(-1, 1, 1);
                // sprite.flipX = true;
            }

            if(Input.GetKey(KeyCode.D))
            {
                //make localscale x positive
                transform.localScale = new Vector3(1, 1, 1);
                // sprite.flipX = false;
            }
            anim.SetBool("walking", move != Vector3.zero);
        }
        else {
            // Check if the character is moving
            Vector3 moveDirection = HasMoved();
            
            // Do something based on the movement status
            if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(moveDirection.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            anim.SetBool("walking", moveDirection != Vector3.zero);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if(!HasStateAuthority) return;
        if(FindObjectOfType<Timer>().Frozen) return;

        move = new(
            Input.GetAxis("Horizontal") * Runner.DeltaTime * stats.moveSpeed,
            Input.GetAxis("Vertical") * Runner.DeltaTime * stats.moveSpeed,
            0
        );

        if(move != Vector3.zero) gameObject.transform.position += move;
    }

    private Vector3 HasMoved()
    {
        // Calculate the movement direction by subtracting previous position from current position
        Vector3 moveDirection = transform.position - previousPosition;

        // Update the previous position
        previousPosition = transform.position;

        return moveDirection;
    }
}
