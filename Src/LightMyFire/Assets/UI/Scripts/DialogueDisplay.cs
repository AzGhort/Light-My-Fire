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
		[SerializeField] private TMP_Text actorName;
		[SerializeField] private TMP_Text textField;
		[SerializeField] private StringEvent dialogResultString;

		private Animator animator;

		private Dictionary<int, Sentence> sentences = new Dictionary<int, Sentence>();
		private Sentence currentSentence = null;

		bool dialogueRunning = false;

		public void AddListener(UnityAction<string> listener) {
			dialogResultString.AddListener(listener);
		}

		private void Awake() {
			animator = GetComponent<Animator>();
			Debug.Assert(animator);
		}

		private void Update() {
			if (!dialogueRunning) { return; }
			if (currentSentence.answers == null) {
				if (Input.GetButtonDown("Interact")) {
					currentSentence = sentences[currentSentence.followingId];
					writeSentence(currentSentence);
				}
				return;
			}

			int option = 100;
			if (Input.GetKeyDown("1")) { option = 1; }
			if (Input.GetKeyDown("2")) { option = 2; }
			if (Input.GetKeyDown("3")) { option = 3; }
			if (Input.GetKeyDown("4")) { option = 4; }

			if (option > currentSentence.answers.Count) { return; }
			currentSentence = sentences[currentSentence.answers[option - 1].followSentenceId];
			writeSentence(currentSentence);
		}

		public void StartPopUp(string popUp) {
			textField.text = popUp;
			animator.SetTrigger("StartPopUp");
		}

		public void EndPopUp() {
			animator.SetTrigger("EndPopUp");
		}

		public void StartDialogue(TextAsset dialogueText) {
			loadDialogue(dialogueText);
			dialogueRunning = true;

			currentSentence = sentences[1];
			writeSentence(currentSentence);

			animator.SetTrigger("Start");
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

				animator.SetTrigger("End");
				GameState.PlayerFrozen = false;

				dialogResultString.Invoke(sentence.endInfo);
				return;
			}

			actorName.text = sentence.actor;

			if (sentence.answers == null) { textField.text = sentence.text; }
			else {
				string finalText = sentence.text + "\n";
				for (int i = 0; i < sentence.answers.Count; i++) {
					finalText += "\n" + (i + 1) + ") " + sentence.answers[i].text;
				}
				textField.text = finalText;
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