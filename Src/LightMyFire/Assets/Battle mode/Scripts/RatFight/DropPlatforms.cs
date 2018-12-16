using UnityEngine;

namespace LightMyFire
{
	public class DropPlatforms : MonoBehaviour
	{

		[SerializeField] private GameObject platforms;

		private int standAloneLayerId;

		public void StartScript()
        {
			foreach (Transform child in platforms.transform)
            {
				child.gameObject.layer = standAloneLayerId;

				var r2d2 = child.gameObject.AddComponent<Rigidbody2D>();
				r2d2.AddForceAtPosition(Random.insideUnitCircle, Random.insideUnitCircle);
			}
            var roots = gameObject.scene.GetRootGameObjects();
            foreach (GameObject go in roots)
            {
                if (go.name == "Rat")
                {
                    go.GetComponent<RatAI>().Stop();
                    go.GetComponent<RatAI>().TransformToPhaseTwo();
                }
            }

			Destroy(gameObject);
		}

		private void Awake() {
			standAloneLayerId = LayerMask.NameToLayer("Standalone Layer");

            gameObject.SetActive(false);
			Debug.Assert(standAloneLayerId != -1);
			Debug.Assert(platforms);
		}

	}
}