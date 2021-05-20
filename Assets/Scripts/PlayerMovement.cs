using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public int potionModAmount = 0;

    public AudioClip jumpClip;
    public AudioClip shootClip;
    public AudioClip ultClip;

    public AudioClip onHitClip;
    public AudioClip onHitClip2;

    private float potionTimeMax = 10f;
    private float potionTimeCur = 0f;

    bool jumpFlag = false;
    bool jump = false;

    float horizontalMove = 0f;

    public GameObject normalAttack;
    public GameObject ultAttack;
    public int ultPoints = 100;
    public float bulletSpeed;
    public float ultSpeed;
    private int timer = 0;

    public int healthPoints = 8;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public GameObject ultReady;
    public GameObject[] hearts = new GameObject[4];
    private int invincibleTime = 0;
    private bool hasLost = false;

    void Start()
    {
        ResetHP();
    }
    // Update is called once per frame
    void Update()
    {
        if (ultPoints >= 20)
        {
            ultReady.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            ultReady.GetComponent<SpriteRenderer>().enabled = false;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (animator.GetBool("IsJumping") == false)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }

        if (timer > 0)
        {
            timer--;
        }
        if (invincibleTime > 0)
        {
            invincibleTime--;
        }
        Attack();
    }

    private void CheckHP()
    {
        Debug.Log("HEALTH POINTS: " + healthPoints);
        if (healthPoints == 7)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (healthPoints == 6)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (healthPoints == 5)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[2].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (healthPoints == 4)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (healthPoints == 3)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[1].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (healthPoints == 2)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (healthPoints == 1)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[0].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        if (healthPoints == 0)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hearts[0].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            hasLost = true;
        }
        if (hasLost)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    private void ResetHP()
    {
        healthPoints = 8;
        for (int i = 0; i < 4; i++)
        {
            hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
        }
        hearts[0].transform.position = new Vector3(-8, 4, -1);
        hearts[1].transform.position = new Vector3(-7.5f, 4, -1);
        hearts[2].transform.position = new Vector3(-7, 4, -1);
        hearts[3].transform.position = new Vector3(-6.5f, 4, -1);
    }

    private void Shoot(GameObject attack, float speed, AudioClip shootClip)
    {
        GameObject clone;
        Rigidbody2D rb;

        Vector3 pos = transform.position;
        Quaternion rotation = transform.rotation;
        clone = Instantiate(attack, pos, rotation);
        rb = clone.GetComponent<Rigidbody2D>();

        AudioSource.PlayClipAtPoint(shootClip, transform.position);

        Vector3 local = transform.localScale;
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

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    void FixedUpdate()
    {
        if (hasJumpPotion && potionTimeCur < potionTimeMax)
        {
            controller.m_JumpForceMod = potionModAmount;
            potionTimeCur += Time.fixedDeltaTime;
        }
        else
        {
            potionTimeCur = 0f;
            controller.m_JumpForceMod = 0;
            hasJumpPotion = false;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
        }

      
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (timer == 0)
            {
                Debug.Log("Shot shot");
                timer = 350;
                // create attack object at player location
                Shoot(normalAttack, bulletSpeed, shootClip);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (timer == 0 && (ultPoints >= 15))
            {
                timer = 350;
                ultPoints = 0;
                Debug.Log("shot ult");
                // create attack object at player location
                Shoot(ultAttack, ultSpeed, ultClip);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile" || collision.gameObject.tag == "Enemy")
        {
            invincibleTime = 1200;
            healthPoints--;
            AudioSource.PlayClipAtPoint(onHitClip, transform.position);
        }
        if(collision.gameObject.tag == "EnemyProjectile2")
        {
            invincibleTime = 1200;
            healthPoints -= 2;
            AudioSource.PlayClipAtPoint(onHitClip2, transform.position);
        }
        CheckHP();
    }

}
