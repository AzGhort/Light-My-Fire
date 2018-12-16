using System.Collections;

using UnityEngine;
using TMPro;

namespace LightMyFire
{
	public class TmpTextCharByChar : MonoBehaviour
	{
		private TMP_Text tmpText;

		void Awake() {
			tmpText = gameObject.GetComponent<TMP_Text>();
			Debug.Assert(tmpText);
		}

		void Start() {
			StartCoroutine(revealCharacters(tmpText));
		}

		private IEnumerator revealCharacters(TMP_Text textComponent) {
			int totalCharacters = textComponent.textInfo.characterCount;
			int visibleCharacters = 0;

			while (true) {
				if (visibleCharacters > totalCharacters) { break; }

				textComponent.maxVisibleCharacters = visibleCharacters;
				++visibleCharacters;

				yield return null;
			}
		}
	}
}