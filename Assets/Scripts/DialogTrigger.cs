using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogeManager>().StartDialogue(dialog);
    }
    private void OnEnable()
    {
        TriggerDialogue();
    }
}
