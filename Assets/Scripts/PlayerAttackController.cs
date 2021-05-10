using System.Collections;
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
<<<<<<< Updated upstream
        if (collision.gameObject.tag == "enemy")
        {
            GameObject.Destroy(this.gameObject);
        }
        /*
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.gameObject, GetComponent<Collider>());
        }
        */
=======
        if (collision.gameObject.tag == "Player")
        {
            //ScoreManagetScript.UpdateScore();
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(collision.gameObject);
        }
>>>>>>> Stashed changes
    }
}
