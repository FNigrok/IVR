using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public int questNumber;
    public Item[] items;
    public Inventory inventory;
    public int count;
    public GameObject[] prizes;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            for(int i = 0; i < inventory.maxCount; i++)
            {
                if (inventory.FindInInventory(items[i], count) == true)
            {
                questNumber++;
                    break;
            }

            }
        }
        CheckQuest();
    }
    public void CheckQuest()
    {
        if(questNumber == 1)
        {
            prizes[0].SetActive(true);
        }
        if(questNumber == 2)
        {
            prizes[1].SetActive(true);
        }
        if (questNumber == 3)
        {
            prizes[2].SetActive(true);
        }
        if (questNumber >= 4)
        {
            prizes[3].SetActive(true);
        }

    }

}
