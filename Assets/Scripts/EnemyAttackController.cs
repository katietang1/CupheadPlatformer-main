using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public AudioClip collisionClip;

    // Update is called once per frame
    void Update()
    {
        //if the attack is off the screen to the right, then it should disappear
        if (Camera.main.WorldToViewportPoint(this.transform.position).x > 1)
            Destroy(this.gameObject);
        else if (Camera.main.WorldToViewportPoint(this.transform.position).x < 0)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "PlayerAttack"  || collision.gameObject.tag == "PlayerUlt")
        {
            GameObject.Destroy(this.gameObject);
            AudioSource.PlayClipAtPoint(collisionClip, transform.position);
        }
    }
}
