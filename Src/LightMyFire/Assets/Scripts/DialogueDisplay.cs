using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    public Text NPCNameText;
    public Text TooltipText;

	// Use this for initialization
	void Start ()
	{
	    SetVisibility(false);
        // TODO: make invisible
    }

    void Update()
    {
        bool fallthrough = true;
        if (node == null)
        {
            FindObjectOfType<Player>().Resume();
        }
        if (node != null && node.DialogueAnswers.Length == 0)
        {
            if (waitingForResponse)
            {
                TooltipText.text = "Stisknete E";
                if (Input.GetKeyDown("e"))
                {
                    fallthrough = false;
                    waitingForAnswer = false;
                    waitingForResponse = false;
                }
            }
            else
            {
                TooltipText.text = "Stisknete E";
                NPCNameText.text = npcName + ':' + '\t' + node.Sentence + '\n';
                if (Input.GetKeyDown("e"))
                {
                    NPCNameText.text = "";
                    TooltipText.text = "";
                    waitingForAnswer = false;
                    waitingForResponse = false;
                    npcName = "";
                    node = null;
                    SetVisibility(false);
                }
            }
        }
        if (node != null && node.DialogueAnswers.Length > 0)
        {
            if (!waitingForAnswer)
            {
                waitingForAnswer = true;
                NPCNameText.text = npcName + ':' + '\t' + node.Sentence + '\n';
                for (int i = 0; i < node.DialogueAnswers.Length; i++)
                {
                    NPCNameText.text += '\t' + (i + 1).ToString() + ") " + node.DialogueAnswers[i].Sentence + '\n';
                }
            }
            else
            {
                if (!waitingForResponse)
                {
                    switch (node.DialogueAnswers.Length)
                    {
                        case 1:
                            TooltipText.text = "Stisknete 1";
                            break;
                        case 2:
                            TooltipText.text = "Stisknete 1 nebo 2";
                            break;
                        case 3:
                            TooltipText.text = "Stisknete 1, 2 nebo 3";
                            break;
                        case 4:
                            TooltipText.text = "Stisknete 1, 2, 3 nebo 4";
                            break;
                    }
                    Dialogue.DialogueNode.DialogueAnswer answer = null;
                    if (Input.GetKeyDown("1"))
                    {
                        answer = node.DialogueAnswers[0];
                    }
                    if (Input.GetKeyDown("2") && node.DialogueAnswers.Length >= 2)
                    {
                        answer = node.DialogueAnswers[1];
                    }
                    if (Input.GetKeyDown("3") && node.DialogueAnswers.Length >= 3)
                    {
                        answer = node.DialogueAnswers[2];
                    }
                    if (Input.GetKeyDown("4") && node.DialogueAnswers.Length >= 4)
                    {
                        answer = node.DialogueAnswers[3];
                    }

                    if (answer != null)
                    {
                        fallthrough = false;
                        NPCNameText.text = "Vajgl:" + '\t' + answer.Sentence;
                        waitingForResponse = true;
                        node = answer.Next;
                    }
                }
                else
                {
                    TooltipText.text = "Stisknete E";
                    if (Input.GetKeyDown("e"))
                    {
                        fallthrough = false;
                        waitingForAnswer = false;
                        waitingForResponse = false;
                    }
                }
            }
        }
        if (!fallthrough)
            Thread.Sleep(200);
    }

    public void StartDialogue(Dialogue d)
    {
        FindObjectOfType<Player>().Stop();
        SetVisibility(true);
        Debug.Log("Conversation");
        node = d.Conversation;
        npcName = d.Name;
    }

    void SetVisibility(bool set)
    {
        GetComponent<SpriteRenderer>().enabled = set;
        NPCNameText.transform.parent.GetComponent<Canvas>().enabled = set;
    }

    private bool waitingForResponse = false;
    private bool waitingForAnswer = false;
    private string npcName;
    private Dialogue.DialogueNode node;
}
