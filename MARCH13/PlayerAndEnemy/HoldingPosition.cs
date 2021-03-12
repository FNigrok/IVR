using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingPosition : MonoBehaviour
{
    public Transform itemHold;
    private Transform player;
    public PlayerController controller;


    //private const float offset = 0.1F;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

 
    void Update()
    {
        if (controller.GetIsRight())
        {
            itemHold.position = new Vector2(player.position.x + 0.08F, player.position.y -0.02F);
        }
        else
        {
            itemHold.position = new Vector2(player.position.x - 0.08F, player.position.y - 0.02F);
        }
    }
}
