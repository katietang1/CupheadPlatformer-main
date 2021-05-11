﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        //if the attack is off the screen to the right, then it should disappear
        if (Camera.main.WorldToViewportPoint(this.transform.position).x > 1)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            // update score here
            GameObject.Destroy(this.gameObject);
        }
        /*
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.gameObject, GetComponent<Collider>());
        }
        */

      
        
    }
}