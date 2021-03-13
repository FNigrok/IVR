using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int cost;
    public Item stuff;
    private PlayerController player;
    public DialogeAnimator anim1;
    public DialogeAnimator anim2;
    public DialogeManager dm;
    private Inventory inventory;
    private bool f = false;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (f)
            {
                anim1.startAnim.SetBool("startOpen", true);
            }
            else
            {
                anim2.startAnim.SetBool("startOpen", true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (f)
            {
                anim1.startAnim.SetBool("startOpen", false);
                dm.EndDialoge();
            }
            else
            {
                anim2.startAnim.SetBool("startOpen", false);
                dm.EndDialoge();
            }
        }
    }

    public void Purchase()
    {

        if(player.money >= cost)
        {
            player.money -= cost;
            inventory.SearchForItem(stuff, 1);
            f = true;
        }
        else
        {
            f = false;
        }
    }
}
