using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelChanger : MonoBehaviour
{

    private Animator anim;
    public int IntLevel;
    public Vector3 pos;
    public PlayerValue player;
    public bool continuePlay;

    public Slider slider;
    public GameObject loadingScreen;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToLevel()
    {
        anim.SetTrigger("fading");
    }

    public void ToLevelComplete()
    {
        if (continuePlay)
        {
            pos = player.playerPos;
        }
        player.playerPos = pos;
        SceneManager.LoadScene(IntLevel);
        StartCoroutine(LoadingScreen());
    } 

    IEnumerator LoadingScreen()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(IntLevel);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.8f);
            slider.value = progress;
            yield return null;
        }

    }
}
