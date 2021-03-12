using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float cooldown;
    public float startCooldown;
    public int health;
    public int attackPoints;
    public float pointPosition;
    public float movespeed;
    public int moneyvalue;
    private PlayerController player1;

    Random r = new Random();

    public GameObject[] prizes = new GameObject[4];

    private bool isRight = false;

    public Animator anim;

    public Transform point;

    public Transform player;

    private bool patrol = false;
    private bool attack = false;
    private bool returning = false;

    public float distance;

    public GameObject effect;

    void Start()
    {
        anim = GetComponent<Animator>();
        player1 = FindObjectOfType<PlayerController>();
    }


    void Update()
    {

        if(health <= 0)
        {
            //Instantiate(prizes[Random.Range(0, 3)], transform.position, Quaternion.identity);
            Destroy(gameObject);
            player1.GiveMoney(moneyvalue);

        }

        if (Vector2.Distance(transform.position, point.position) < pointPosition && attack == false)
        {
            patrol = true;
        }
        if (Vector2.Distance(transform.position, player.position) < distance)
        {
            attack = true;
            patrol = false;
            returning = false;
        }
        if (Vector2.Distance(transform.position, player.position) > distance)
        {
            returning = true;
            attack = false;
        }
        //else
        //{
        //    Flip();
        //}

        if (patrol == true)
        {
            Patrol();
        }
        else if (attack == true)
        {
            Attack();
        }
        else if (returning == true)
        {
            Returning();
        }
    }

    public void TakeDamage(int damage)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        health -= damage;
        if (player.position.x < transform.position.x)
        {
            if (player.position.y <= transform.position.y)
            {
                for (int i = 0; i < 20; i++)
                {
                    transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y + 0.01f);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y + 0.01f);
                }
            }
        }
        else
        {
            if (player.position.y <= transform.position.y)
            {
                for (int i = 0; i < 20; i++)
                {
                    transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y + 0.01f);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y - 0.01f);
                }
            }
        }
    }

    private void Patrol()
    {
        if (transform.position.x > point.position.x + pointPosition && isRight == true)
        {
            Flip();
        }
        else if (transform.position.x < point.position.x - pointPosition && isRight == false)
        {
            Flip();
        }
        if (isRight)
        {
            transform.position = new Vector2(transform.position.x + movespeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - movespeed * Time.deltaTime, transform.position.y);
        }
    }

    private void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movespeed * Time.deltaTime);
    }
    private void Returning()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, movespeed * Time.deltaTime);
    }
    void Flip()
    {
        isRight = !isRight;
        Vector2 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(cooldown <= 0) {
                DealDamage();
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    public void DealDamage()
    {
        Instantiate(effect, player1.transform.position, Quaternion.identity);
        player1.TakeDamage(attackPoints);
        cooldown = startCooldown;
    }
}
