using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    public int chestValue;
    private Animator anim;
    private bool open;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !open)
        {
            anim.SetTrigger("open");
            GameController.instance.UpdateChests(chestValue);
            open = true;
        }
    }
}
