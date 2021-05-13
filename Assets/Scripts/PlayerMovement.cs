using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public bool hasJumpPotion = false;
    public bool hasSpeedPotion = false;
    public int potionModAmount = 0;

    public AudioClip jumpClip;

    private float potionTimeMax = 10f;
    private float potionTimeCur = 0f;

    bool jumpFlag = false;
    bool jump = false;

    float horizontalMove = 0f;

    public GameObject normalAttack;
    public GameObject ultAttack;
    public float bulletSpeed;
    public float ultSpeed;
    private int timer = 0;

    public int healthPoints = 4;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject[] hearts = new GameObject[4];
    private int invincibleTime = 0;

    void Start()
    {
        ResetHP();
    }
    // Update is called once per frame
    void Update()
    {
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
        if (healthPoints == 3)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (healthPoints == 2)
        {
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (healthPoints == 1)
        {
            hearts[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        if (healthPoints == 0)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
    }

    private void ResetHP()
    {
        healthPoints = 4;
        for (int i = 0; i < 4; i++)
        {
            hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
        }
        hearts[0].transform.position = new Vector3(-8, 4, -1);
        hearts[1].transform.position = new Vector3(-7.5f, 4, -1);
        hearts[2].transform.position = new Vector3(-7, 4, -1);
        hearts[3].transform.position = new Vector3(-6.5f, 4, -1);
    }

    private void Shoot(GameObject attack, float speed)
    {
        GameObject clone;
        Rigidbody2D rb;

        Vector3 pos = transform.position;
        Quaternion rotation = transform.rotation;
        clone = Instantiate(attack, pos, rotation);
        rb = clone.GetComponent<Rigidbody2D>();

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
                timer = 400;
                // create attack object at player location
                Shoot(normalAttack, bulletSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (timer == 0)
            {
                timer = 400;
                // create attack object at player location
                Shoot(ultAttack, ultSpeed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile" || collision.gameObject.tag == "Enemy")
        {
            invincibleTime = 1200;
            healthPoints--;
            CheckHP();
        }
    }

}
