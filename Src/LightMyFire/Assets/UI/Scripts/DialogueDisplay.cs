using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace LightMyFire
{
    [RequireComponent(typeof(Animator))]
    public class DialogueDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI actorName;
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private StringEvent dialogResultString;

        private Animator animator;

        private Dictionary<int, Sentence> sentences = new Dictionary<int, Sentence>();
        private Sentence currentSentence = null;

        private int selected = 0;
        private bool dialogueRunning = false;

        public void AddListener(UnityAction<string> listener) {
            dialogResultString.AddListener(listener);
        }

        private void Awake() {
            animator = GetComponent<Animator>();
            Debug.Assert(animator);
        }

        private void Update() {
            if (!dialogueRunning) { return; }
            if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Submit")) {
                if (currentSentence.answers == null) {
                    currentSentence = sentences[currentSentence.followingId];
                }
                else if (selected != 0) {   // Submit selected answer
                    currentSentence = sentences[currentSentence.answers[selected - 1].followSentenceId];
                }
                else { return; }

                selected = 0;
                writeSentence(currentSentence);
            }

            if (currentSentence.answers == null) { return; }

            int option = 100;
            if (Input.GetKeyDown("1")) { option = 1; }
            if (Input.GetKeyDown("2")) { option = 2; }
            if (Input.GetKeyDown("3")) { option = 3; }
            if (Input.GetKeyDown("4")) { option = 4; }

            if (option > currentSentence.answers.Count) { return; } // Ivalid selection
            else if (option == selected) {  // Inputed option is same as selected => submit selected answer
                currentSentence = sentences[currentSentence.answers[selected - 1].followSentenceId];
                selected = 0;
            }
            else { selected = option; } // Same sentence, highlight selected answer

            writeSentence(currentSentence);
        }

        public void StartPopUp(string actor, string popUpText) {
            actorName.text = actor;
            textField.text = popUpText;
            animator.SetBool("PopUpEnd", false);
            animator.SetTrigger("PopUpStart");
        }

        public void EndPopUp() {
            animator.SetBool("PopUpEnd", true);
        }

        public void StartDialogue(TextAsset dialogueText) {
            loadDialogue(dialogueText);
            dialogueRunning = true;

            currentSentence = sentences[1];
            writeSentence(currentSentence);

            animator.SetTrigger("DialogueStart");
            GameState.PlayerFrozen = true;
        }

        private void loadDialogue(TextAsset dialogueText) {
            sentences = new Dictionary<int, Sentence>();

            using (StringReader reader = new StringReader(dialogueText.text)) {
                while (true) {
                    string line = reader.ReadLine();
                    if (line == null) { break; }
                    if (line == "") { continue; }
                    string[] sentenceInfo = line.Split(' ');

                    Sentence sentence = new Sentence {
                        id = int.Parse(sentenceInfo[0])
                    };

                    if (sentenceInfo.Length == 2) {
                        sentence.endInfo = sentenceInfo[1];
                        sentences[sentence.id] = sentence;
                        continue;
                    }

                    sentence.actor = sentenceInfo[2] == "_" ? "" : sentenceInfo[2];
                    sentence.text = reader.ReadLine();

                    if (sentenceInfo[1][0] == '*') {
                        sentence.answers = new List<Answer>();

                        while (true) {
                            line = reader.ReadLine();
                            if (line == null) { return; }
                            if (line == "") { break; }
                            string[] answerInfo = line.Split(' ');

                            Answer answer = new Answer {
                                followSentenceId = int.Parse(answerInfo[1]),
                                text = reader.ReadLine()
                            };
                            sentence.answers.Add(answer);
                        }
                    }
                    else { sentence.followingId = int.Parse(sentenceInfo[1]); }

                    sentences[sentence.id] = sentence;
                }
            }
        }
        private void writeSentence(Sentence sentence) {
            if (sentence.text == null) {
                dialogueRunning = false;

                animator.SetTrigger("DialogueEnd");
                GameState.PlayerFrozen = false;

                dialogResultString.Invoke(sentence.endInfo);
                return;
            }

            actorName.SetText(sentence.actor);

            if (sentence.answers == null) { textField.SetText(sentence.text); }
            else {
                string finalText = sentence.text + "\n";
                for (int i = 0; i < sentence.answers.Count; i++) {
                    if (selected == i + 1) {
                        finalText += "\n" + "<b>" + (i + 1) + ") " + sentence.answers[i].text + "</b>";
                    }
                    else { finalText += "\n" + (i + 1) + ") " + sentence.answers[i].text; }
                }
                textField.SetText(finalText);
            }
        }

        private class Sentence
        {
            public string actor = null;
            public string text = null;
            public string endInfo = null;

            public int id;
            public int followingId = -1;
            public List<Answer> answers = null;
        }

        private class Answer
        {
            public string text;
            public int followSentenceId;
        }
    }
}