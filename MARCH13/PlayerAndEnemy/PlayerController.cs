using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    public int maxhp;
    public int health;
    public int money;

    public Text moneyScore;
    public Text hpScore;

    public float movespeed = 3;
    private float moveInputX, moveInputY;

    private Vector2 moveVelocity;

    private Rigidbody2D rb;

    private bool isRight = true;
    public InventoryBase inventoryBase;
    public Inventory inventory;

    public PlayerValue playerValue;

    private Enemy enemy;

    public GameObject effect;
    public GameObject deathScene;

    void Start()
    {
        money = playerValue.money;
        maxhp = playerValue.health;
        transform.position = playerValue.playerPos;
        enemy = FindObjectOfType<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        moneyScore.text = money.ToString();
        hpScore.text = health.ToString();
        if (health <= 0)
        {
            deathScene.SetActive(true);
            Destroy(gameObject);
            for (int i = 0; i < inventory.maxCount; i++)
            {
                if (inventory.items[i].id != 0)
                {
                    Instantiate(inventory.items[i].itemGameObject, transform.position, Quaternion.identity);
                }
            }
            }

            moveInputX = Input.GetAxisRaw("Horizontal");
            moveInputY = Input.GetAxisRaw("Vertical");
            Vector2 moveInput = new Vector2(moveInputX, moveInputY);
            moveVelocity = moveInput.normalized * movespeed;
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
            if (isRight == false && moveInputX > 0)
            {
                Flip();
            }
            else if (isRight == true && moveInputX < 0)
            {
                Flip();
            }
            if (moveInputX > 0 || moveInputX < 0)
            {
                anim.SetBool("walking", true);
                anim.SetBool("walkingUp", false);
                anim.SetBool("walkingDown", false);
                anim.SetBool("idle", false);
            }
            else if (moveInputY > 0 && moveInputX == 0)
            {
                anim.SetBool("walkingUp", true);
                anim.SetBool("walking", false);
                anim.SetBool("walkingDown", false);
                anim.SetBool("idle", false);
            }
            else if (moveInputY < 0 && moveInputX == 0)
            {
                anim.SetBool("walkingDown", true);
                anim.SetBool("walking", false);
                anim.SetBool("walkingUp", false);
                anim.SetBool("idle", false);
            }
            else
            {
                anim.SetBool("idle", true);
            }

        }
        void Flip()
        {
            isRight = !isRight;
            Vector2 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }

        public bool GetIsRight()
        {
            return isRight;
        }

        public void TakeDamage(int damage)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            health -= damage;
            if (enemy.transform.position.x < transform.position.x)
            {
                if (enemy.transform.position.y <= transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.2f);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f);
                }
            }
            else
            {
                if (enemy.transform.position.y <= transform.position.y)
                {
                    transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y + 0.2f);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y - 0.2f);
                }
            }
        }
        
        public void GiveMoney(int amount)
        {
            money += amount;
        }

        public void Heal(int amount)
        {
            if(health + amount >= maxhp)
            {
                health = maxhp;
            }
            else
            {
                health += amount;
            }
        }
        
        public void SetMaxHp(int amount) 
        {
            maxhp = amount;
        }








    }
