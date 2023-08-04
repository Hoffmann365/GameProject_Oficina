using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float speed;
    public float walktime;
    public int damage = 1;
    
    private float timer;
    
    public int health;

    private bool walkRight = true;
    private bool alive = true;
    
    private Rigidbody2D rig;
    private Animator anim;
    private BoxCollider2D colliderEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colliderEnemy = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= walktime)
        {
            walkRight = !walkRight;
            timer = 0f;
        }

        if (walkRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.right * speed;
            anim.SetInteger("transition", 1);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.left * speed;
            anim.SetInteger("transition", 1);
        }
        
        
    }
    public void Damage(int dmg)
    {
        anim.SetTrigger("hit");
        health -= dmg;
        if (health <= 0)
        {
            StartCoroutine("die");
        }
    }

    IEnumerator die()
    {
        alive = false;
        rig.gravityScale = 0;
        colliderEnemy.isTrigger = true;
        speed = 0;
        anim.SetTrigger("die");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive == true)
        {
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
}
