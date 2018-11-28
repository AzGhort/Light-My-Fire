using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public string Name;

    [System.Serializable]
    public class DialogueNode
    {
        [TextArea]
        public string Sentence;

        [System.Serializable]
        public class DialogueAnswer
        {
            [TextArea]
            public string Sentence;
            public DialogueNode Next;
        }

        public DialogueAnswer[] DialogueAnswers;
    }

    public DialogueNode Conversation;

    void Start()
    {
        display = FindObjectOfType<DialogueDisplay>();
    }

    public void Engage()
    {
        if (Interlocked.CompareExchange(ref dialogueDone, 1, 0) == 0)
            display.StartDialogue(this);
    }

    private int dialogueDone = 0;
    private DialogueDisplay display;
}
