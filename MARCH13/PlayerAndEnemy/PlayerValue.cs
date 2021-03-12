using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]

public class PlayerValue : ScriptableObject
{
    public Vector3 playerPos;
    public InventoryBase playerBase;
    public Inventory playerInventory;
    public int money;
    public int health;
    
    

    public void SetPlayerpos(Vector3 vector)
    {
        playerPos = vector;
    }
    public void SetInventory()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
}
