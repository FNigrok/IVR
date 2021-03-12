using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private int currentMenu = 0;
    public GameObject[] menu;

    public void NextFrame(int changeMenu)
    {
        menu[currentMenu].SetActive(false);
        menu[changeMenu].SetActive(true);
        currentMenu = changeMenu;
    }

}
