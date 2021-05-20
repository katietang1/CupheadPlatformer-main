using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        //if the attack is off the screen to the right, then it should disappear
        if (Camera.main.WorldToViewportPoint(this.transform.position).x > 1)
            Destroy(this.gameObject);
        else if (Camera.main.WorldToViewportPoint(this.transform.position).x < 0)
            Destroy(this.gameObject);
    }

    public void changeDirection(bool isRight)
    {
        rb = GetComponent<Rigidbody2D>();
        if (isRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            Debug.Log("reverse");
            rb.velocity = new Vector2(-4, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Platforms" || collision.gameObject.tag == "EnemyProjectile" || collision.gameObject.tag == "EnemyProjectile2")
        {
            GameObject.Destroy(this.gameObject);
        }

    }
}
