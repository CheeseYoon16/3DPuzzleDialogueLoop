using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartDialogueInteraction : MonoBehaviour
{
    [SerializeField] DialogueSeqData dialogueSequenceData = null;
    public UnityEvent onConversationFinished;

    public void TriggerDialogue()
    {
        if(dialogueSequenceData != null && DialogueManager.Instance!=null)
        {
            DialogueManager.Instance.ConversationStart(dialogueSequenceData, () =>
            {
                onConversationFinished?.Invoke();
            });
        }
    }
}
