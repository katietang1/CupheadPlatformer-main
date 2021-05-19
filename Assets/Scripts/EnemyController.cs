using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int enemyHealth = 80;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public GameObject[] hearts = new GameObject[4];
    private int invincibleTime = 0;

    public PlayerMovement player;

    public AudioClip hitClip;
    public AudioClip fireClip1;
    public AudioClip fireClip2;

    private int timer = 0;

    public GameObject normalAttack;
    public GameObject secondAttack;
    public float bulletSpeed;
    private int bulletTimer = 0;
    private int bulletTimer2 = 400;

    public Vector3 startpos;
    public Vector3 corner2;
    public Vector3 corner3;
    public Vector3 corner4;
    private bool facingright = false;

    private bool hasWon = false;

    void Start()
    {
        ResetHP();
        transform.position = startpos;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer--;
        }
        if (invincibleTime > 0)
        {
            invincibleTime--;
        }
        if (bulletTimer > 0)
        {
            bulletTimer--;
        }
        if (bulletTimer2 > 0)
        {
            bulletTimer2--;
        }


        if ((transform.position - startpos).sqrMagnitude < 0.01f)
        {
            StopAllCoroutines();
            StartCoroutine(MoveTowards(transform.position, corner2, 3f));
        }
        else if ((transform.position - corner2).sqrMagnitude < 0.01f)
        {
            StopAllCoroutines();
            StartCoroutine(MoveTowards(transform.position, corner3, 3f));
        }
        else if ((transform.position - corner3).sqrMagnitude < 0.01f)
        {
            StopAllCoroutines();
            StartCoroutine(MoveTowards(transform.position, corner4, 3f));
        }
        else if ((transform.position - corner4).sqrMagnitude < 0.01f)
        {
            StopAllCoroutines();
            StartCoroutine(MoveTowards(transform.position, startpos, 3f));
        }

        Attack();
    }

    private void CheckHP()
    {
        if (enemyHealth < 75 && enemyHealth >= 70)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (enemyHealth < 70 && enemyHealth >= 60)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (enemyHealth < 60 && enemyHealth >= 50)
        {
            hearts[1].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (enemyHealth < 50 && enemyHealth >= 40)
        {
            hearts[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (enemyHealth < 40 && enemyHealth >= 30)
        {
            hearts[2].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (enemyHealth < 20 && enemyHealth >= 10)
        {
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (enemyHealth < 10 && enemyHealth >= 0)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        if (enemyHealth == 0)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hasWon = true;
        }
    }
    private void ResetHP()
    {
        enemyHealth = 80;
        for (int i = 0; i < 4; i++)
        {
            hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
        }
        hearts[0].transform.position = new Vector3(8, 4, 0);
        hearts[1].transform.position = new Vector3(7.5f, 4, 0);
        hearts[2].transform.position = new Vector3(7, 4, 0);
        hearts[3].transform.position = new Vector3(6.5f, 4, 0);
    }

    void Attack()
    {
        if(bulletTimer == 0)
        {
            Shoot(normalAttack, bulletSpeed, fireClip1);
            bulletTimer = 2000;
            Debug.Log("shot enemy");
        }
        if(bulletTimer2 == 0)
        {
            Shoot(secondAttack, bulletSpeed, fireClip2);
            bulletTimer2 = 1000;
        }
    }

    private void Shoot(GameObject attack, float speed, AudioClip fireClip)
    {
        GameObject clone;
        Rigidbody2D rb;

        Vector3 pos = transform.position;
        Quaternion rotation = transform.rotation;
        clone = Instantiate(attack, pos, rotation);
        rb = clone.GetComponent<Rigidbody2D>();

        AudioSource.PlayClipAtPoint(fireClip, transform.position);

        Vector3 local = transform.localScale;
        Debug.Log(transform.localScale.x);
        if (local.x > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
            Vector3 theScale = clone.transform.localScale;
            theScale.x *= -1;
            clone.transform.localScale = theScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            invincibleTime = 1200;
            enemyHealth -= 2;
            player.ultPoints++;
            CheckHP();
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
        }

        if (collision.gameObject.tag == "PlayerUlt")
        {
            invincibleTime = 1200;
            enemyHealth -= 5;
            CheckHP();
            AudioSource.PlayClipAtPoint(hitClip, transform.position);
        }
    }

    IEnumerator MoveTowards(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            DirectionCheck(destination);
            this.transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void DirectionCheck(Vector3 destination)
    {
        if (destination.x > transform.position.x && !facingright)
        {
            facingright = true;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (destination.x < transform.position.x && facingright)
        {
            facingright = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void Won()
    {
        if (hasWon)
        {
            //Play enemy death animation
            //Play won sound
            //SceneManager.LoadScene("WonScene");
        }
    }
}
