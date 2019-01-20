using System.Collections;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class OnChangeHealthHighlight : MonoBehaviour {

	[SerializeField] float highlightSeconds = 0.25f;
	[SerializeField] private Color healthReduced;
	[SerializeField] private Color healthRecovered;

	private SpriteRenderer spriteRenderer;
	private Color baseColor;

	private float healthChange = 0;
	private int coroutinesRunning = 0;

	public void ChangeOfHealthHandler(float changeOfHealth) {
		if (changeOfHealth != 0) {
			healthChange = changeOfHealth;
			++coroutinesRunning;
			StartCoroutine(highlightCoroutine());
		}
	}

	private IEnumerator highlightCoroutine() {
		spriteRenderer.color = healthChange < 0 ? healthReduced : healthRecovered;

		yield return new WaitForSeconds(highlightSeconds);

		if (--coroutinesRunning == 0) { spriteRenderer.color = baseColor; }
	}

	private void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Assert(spriteRenderer);

		baseColor = spriteRenderer.color;
	}
}
