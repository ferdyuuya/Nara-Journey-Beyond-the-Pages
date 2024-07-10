using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spr; //Get the sprite renderer component
    public LayerMask Ground;

    const float groundCheckRadius = 0.2f;

    bool isGrounded = true;

    private Animator anim; //Animator component

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal"); //Get the input from the player
        anim = GetComponent<Animator>(); //Get the animator component

         

        //Flip the sprite when the player is moving to the left
        if (input < 0)
        {
            spr.flipX = true;
        } else if (input > 0)
        {
            spr.flipX = false;
        }

        //For detect if the player is walking or not to change the animation
        if (input == 0)
        {
            if (anim != null) 
            {
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            if (anim != null) 
            {
                anim.SetBool("isWalking", true);
            }
        }
    }
    void FixedUpdate()
    {
       GroundCheck();

        if (CanMove() == false)
            return;

        //Move the player
        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);
    }

    public void Walking()
    {
        Debug.Log("Player is walking");
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
    }

    bool CanMove()
    {
        //bool can = true;

        InteractionSystem interactionSystem = FindAnyObjectByType<InteractionSystem>();
        return !(interactionSystem != null && interactionSystem.isWindowActive);

        //if (FindAnyObjectByType<InventorySystem>().isOpen)
        //{
        //    can = false;
        //}
        //return can;
    }
}