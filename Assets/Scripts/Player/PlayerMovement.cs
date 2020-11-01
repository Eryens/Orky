using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private bool isWalking;
    private bool isFacingRight = true;
    private Animator animator;
    private bool isCrouching;
    public bool canMove = true;
    public bool canCrouch = true;

    private Transform interractFrame;

    public float moveSpeed = 2.0f;

    private Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //rb.freezeRotation = true; // L'objet ne rotate pas quand il entre en collision

        interractFrame = transform.Find("InteractFrame");
        if (interractFrame == null)
        {
            Debug.LogError("There is no 'InteractFrame' object as a child of the player");
        }
    }

    void Update()
    {
        if (canCrouch)
        {
            if (Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.E)) 
            {
                StartPlayerCrouch();
            }
            else if (Input.GetKeyUp(KeyCode.Joystick1Button2) || Input.GetKeyUp(KeyCode.E))
            {
                StopPlayerCrouch();
            }
        }

    }

    private void StopPlayerCrouch()
    {
        animator.SetBool("Crouch", false);
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(0.71f, 0.9f);
        collider.offset = new Vector2(0, 0);
        canMove = true;
    }

    private void StartPlayerCrouch()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(0.6f, 0.7f);
        collider.offset = new Vector2(0.01f, -0.1f);
        animator.SetBool("Crouch", true);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        canMove = false;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveVertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            
            WalkingAnimation(moveHorizontal, moveVertical);
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f).normalized;
            //rb.AddForce(movement * moveSpeed);
            gameObject.GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;
            //transform.Translate(new Vector3(moveHorizontal, moveVertical));
        }
    }


    private void WalkingAnimation(float moveHorizontal, float moveVertical)
    {
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            isWalking = true;
            animator.SetBool("Walking", isWalking);
        }
        else
        {
            isWalking = false;
            animator.SetBool("Walking", isWalking);
        }
        if (moveHorizontal < 0 && isFacingRight)
        {
            Flip();
        }
        if (moveHorizontal > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 flippedCharacter = transform.localScale;
        flippedCharacter.x *= -1;
        transform.localScale = flippedCharacter;

        Vector3 flippedInterractFrame = interractFrame.localScale;
        flippedInterractFrame.x *= -1;
        interractFrame.localScale = flippedInterractFrame;
    }
}
