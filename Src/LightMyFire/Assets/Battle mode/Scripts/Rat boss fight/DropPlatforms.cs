using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TentativeMaster
{
	public class DropPlatforms : MonoBehaviour
	{

		[SerializeField] private GameObject platforms;

		private Interactible interactible;
		private int standAloneLayerId;

		public void StartScript() {
			foreach (Transform child in platforms.transform) {
				child.gameObject.layer = standAloneLayerId;

				var r2d2 = child.gameObject.AddComponent<Rigidbody2D>();
				r2d2.AddForceAtPosition(Random.insideUnitCircle, Random.insideUnitCircle);
			}

			//Destroy(interactible); // To let object live but make event untriggerable again
			Destroy(gameObject);
		}

		private void Start() {
			standAloneLayerId = LayerMask.NameToLayer("Standalone Layer");
			interactible = gameObject.GetComponent<Interactible>();

			Debug.Assert(standAloneLayerId != -1);
			Debug.Assert(interactible != null);
			Debug.Assert(platforms != null);
		}

	}
}