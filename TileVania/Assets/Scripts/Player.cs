using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);


    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    float gravityStart;
    BoxCollider2D Feet;
    

    // Message then methods
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        gravityStart = myRigidbody.gravityScale;
        Feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        { 
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        Death();
        }
    }

    private void Death()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Trap")))                 
        {
            isAlive = false;
            myAnimator.SetTrigger("Death");
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }

    private void Run()
    {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // Value between -1 to 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHorizonSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizonSpeed);
        
    }

    private void Jump()
    {
        if (Feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
                myRigidbody.velocity += jumpVelocity;
            }
        }
    }


    private void FlipSprite()
    {
        bool playerHorizonSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHorizonSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void ClimbLadder()
    {
        //if touch ladder then you can climb
        if (!Feet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityStart;
            return; }
        
        float climbVector = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x , climbVector * climbSpeed);
        myRigidbody.velocity = playerVelocity;
        bool playerVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerVerticalSpeed);
        myRigidbody.gravityScale = 0;
        
    }
}
