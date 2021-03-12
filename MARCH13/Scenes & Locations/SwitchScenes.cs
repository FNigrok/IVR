using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScenes : MonoBehaviour
{
    public int id;
    public GameObject[] otherScenes;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<PlayerController>())
        {
            if (other.CompareTag("Player"))
            {
                for(int i = 0; i < otherScenes.Length; i++)
                {
                    if (i == id)
                    {
                        otherScenes[i].SetActive(true);
                    }
                    else
                    {
                        otherScenes[i].SetActive(false);
                    }
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (FindObjectOfType<PlayerController>())
        {
            if (other.CompareTag("Player"))
            {
                otherScenes[id].SetActive(false);
            }
        }
    }
}
