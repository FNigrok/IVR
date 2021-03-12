using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float cooldown;
    public float startCooldown;
    public float attackRange;

    public int damage;

    public Transform attackPos;

    public LayerMask enemy;

    public Animator anim;

    void Start()
    {
        
    }

    void Update()
    {
        if (cooldown <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                anim.SetTrigger("isTriggered");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                cooldown = startCooldown;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
