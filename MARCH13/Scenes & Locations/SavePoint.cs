using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Transform pos;
    private Vector3 savePosition;
    public PlayerValue player;
    public DialogeAnimator anim;
    public DialogeManager dm;
    public Inventory inventory;
    public int money;
    public int health;
    public int maxhp;
    private PlayerController player1;
    void Start()
    {
        player1 = FindObjectOfType<PlayerController>();
        savePosition.x = pos.position.x;
        savePosition.y = pos.position.y;
        savePosition.z = pos.position.z;

    }
    private void Update()
    {
        money = player1.money;
        health = player1.health;
        maxhp = player1.maxhp;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.startAnim.SetBool("startOpen", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        anim.startAnim.SetBool("startOpen", false);
        dm.EndDialoge();
    }

    public void Save()
    {
        player.money = money;
        player.health = maxhp;
        player.SetPlayerpos(savePosition);
        player.SetInventory();
    }


}
