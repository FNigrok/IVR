using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator[] chests;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Animator anim in chests)
            {
                anim.SetTrigger("isTriggered");
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Animator anim in chests)
            {
                anim.SetTrigger("isTriggered");
            }
        }
    }


}
