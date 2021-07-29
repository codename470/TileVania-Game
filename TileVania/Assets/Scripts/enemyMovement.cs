using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D myRigid;
    BoxCollider2D wallDetect;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        wallDetect = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigid.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigid.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigid.velocity.x)), 1f);
    }

}

//My try for challenge.
/*  private void Move()
   {

       if (wallDetect.IsTouchingLayers(LayerMask.GetMask("Ground")))
       {
           check = !check;
           myRigid.velocity = new Vector2(-moveSpeed, 0);
       }
       else
       {
           if(check)
           { 
           myRigid.velocity = new Vector2(moveSpeed, 0);
           }
           else
           { 
           myRigid.velocity = new Vector2(-moveSpeed, 0);
           }

       }
   }

   private void FlipSprite()
   {
       bool enemyHorizonSpeed = Mathf.Abs(myRigid.velocity.x) > Mathf.Epsilon;
       if (enemyHorizonSpeed)
       {
           transform.localScale = new Vector2(Mathf.Sign(myRigid.velocity.x), 1f);

       }
   }
   */
