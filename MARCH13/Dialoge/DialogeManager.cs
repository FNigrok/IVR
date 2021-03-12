using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogeManager : MonoBehaviour
{
    public Text dialogeText;
    public Text nameText;

    

    public Animator boxAnim;
    public Animator startAnim;

    private Queue<string> sentences;
    private void Start()
    {
        sentences = new Queue<string>();
    }
    public void startDialoge(Dialoge dialoge)
    {
        boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);
        nameText.text = dialoge.name;
        sentences.Clear();
        

        foreach(string sentence in dialoge.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialoge();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogeText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogeText.text += letter;
            yield return null;
        }
    }

    public void EndDialoge()
    {
        boxAnim.SetBool("boxOpen", false);
    }
}
