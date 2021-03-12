using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogetrigger : MonoBehaviour
{
    public Dialoge dialoge;

    public void TriggerDialoge()
    {
        FindObjectOfType<DialogeManager>().startDialoge(dialoge);
    }
}
