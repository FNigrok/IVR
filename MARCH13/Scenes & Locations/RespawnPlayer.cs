using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RespawnPlayer : MonoBehaviour
{
    public Text text1;
    public Text text2;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        text1.text = ("Вы умерли?");
        text2.text = ("Нажмите escape, чтобы вернуться к жизни.");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
