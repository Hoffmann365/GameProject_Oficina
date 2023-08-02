using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float s;
    public float jumpForce;
    
    private bool isJumping;
    private bool doublejump;
    private bool isAtk;
    
    public static float movement;
    
    private Rigidbody2D rig;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        s = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
    }

    void FixedUpdate()
    {
        Move();
        
    }
    
    void Move()
    {
        movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0)
        {
            if(!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if (movement < 0)
        {
            if(!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
    
            transform.eulerAngles = new Vector3(0,180,0);
        }
        if(movement == 0 && !isJumping && !isAtk)
        {
            anim.SetInteger("transition", 0);
        }
    }
    
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doublejump = true;
                isJumping = true;
                
            }
            else
            {
                if(doublejump)
                {
                    anim.SetInteger("transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce * 1), ForceMode2D.Impulse);
                    doublejump = false;
                }
            }        
        }

    }

    void Attack()
    {
        StartCoroutine("Atk");
    }
    
    IEnumerator Atk()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAtk = true;
            anim.SetInteger("transition", 3);
            anim.SetBool("atk", true);
            yield return new WaitForSeconds(5f);
            
        }
    }

    void EndAnimationATK()
    {
        isAtk = false;
        anim.SetBool("atk", false);
        anim.SetInteger("transition", 0);
    }

    
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
    
    
}