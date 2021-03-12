using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogeAnimator : MonoBehaviour
{
    public Animator startAnim;
    public DialogeManager dm;

    public void OnTriggerEnter2D(Collider2D other)
    {
        startAnim.SetBool("startOpen", true);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        startAnim.SetBool("startOpen", false);
        dm.EndDialoge();
    }
}
