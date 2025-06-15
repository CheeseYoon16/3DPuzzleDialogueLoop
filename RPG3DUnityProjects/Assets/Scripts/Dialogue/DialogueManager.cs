using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;

    private Queue<string> sentences;
    private int currentConversationindex = 0;
    private DialogueSeqData trackConversation;
    private Coroutine currentDialogueCorout = null;
    private Coroutine typeSentenceCorout = null;

    static DialogueManager instance;

    public static DialogueManager Instance => instance;

    [SerializeField] GameObject dialogueBox = null;
    UnityAction onConversationFinished = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(instance);
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void ConversationStart(DialogueSeqData dialogue, UnityAction conversationFinishedAction = null)
    {
        trackConversation = dialogue;

        if(currentDialogueCorout == null)
        {
            DialogueData FirstDialogue = trackConversation.listOfDialogue[currentConversationindex];

            onConversationFinished = conversationFinishedAction;

            currentDialogueCorout = StartCoroutine(StartDialogueCorout(FirstDialogue, EndDialogue));
        }

    }

    private IEnumerator StartDialogueCorout(DialogueData dialogue, UnityAction oncomplete)
    {
        if(dialogueBox != null)
        {
            dialogueBox.SetActive(true);
        }

        yield return new WaitForSeconds(0.1f);

        StartDialogue(dialogue, oncomplete);
    }

    private void StartDialogue(DialogueData dialogue, UnityAction oncomplete)
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if(typeSentenceCorout != null)
        {
            StopCoroutine(typeSentenceCorout);
        }

        typeSentenceCorout = StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence) 
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        typeSentenceCorout = null;
    }

    void EndDialogue() 
    {
        animator.SetBool("isOpen", false);

        currentConversationindex += 1;
        if (currentConversationindex < trackConversation.listOfDialogue.Count) 
        {
            DialogueData nextDialogue = trackConversation.listOfDialogue[currentConversationindex];
            StartDialogue(nextDialogue, EndDialogue);
        }
        else
        {
            if (dialogueBox != null)
            {
                dialogueBox.SetActive(false);
            }

            currentConversationindex = 0;
            onConversationFinished?.Invoke();

            onConversationFinished = null;
            currentDialogueCorout = null;

            Debug.Log("Conversation End");
        }
    }
}
