using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : MonoBehaviour
{
    public int questcomplete;
    public int hp;
    private int hpfinal;
    public Quests quest;
    public DialogeAnimator anim;
    public DialogeManager dm;
    public GameObject prize;

    private void Start()
    {
        hpfinal = hp - 1;
    }

    void Update()
    {
        if (quest.questNumber >= questcomplete)
        {
            hp = hpfinal;
        }
            if (hp == 0)
        {
            Destroy(gameObject);
        }   
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(quest.questNumber >= questcomplete)
        {
            anim.startAnim.SetBool("startOpen", true);
            prize.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        anim.startAnim.SetBool("startOpen", false);
        dm.EndDialoge();
    }
}
