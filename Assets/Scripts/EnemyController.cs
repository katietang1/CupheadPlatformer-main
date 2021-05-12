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

    private int timer = 0;
    
    void Start()
    {
        ResetHP();

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
        
    }

    private void CheckHP()
    {
        if (enemyHealth == 70)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (enemyHealth == 60)
        {
            hearts[0].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (enemyHealth == 50)
        {
            hearts[1].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (enemyHealth == 40)
        {
            hearts[1].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (enemyHealth == 30)
        {
            hearts[2].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        else if (enemyHealth == 20)
        {
            hearts[2].GetComponent<SpriteRenderer>().sprite = emptyHeart;
        }
        else if (enemyHealth == 10)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = halfHeart;
        }
        if (enemyHealth == 0)
        {
            hearts[3].GetComponent<SpriteRenderer>().sprite = emptyHeart;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            invincibleTime = 1200;
            enemyHealth -= 2;
            CheckHP();
        }

        if (collision.gameObject.tag == "PlayerUlt")
        {
            invincibleTime = 1200;
            enemyHealth -= 5;            
            CheckHP();
        }
    }
}
