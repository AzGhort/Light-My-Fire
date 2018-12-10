using UnityEngine;

namespace LightMyFire
{
	[RequireComponent(typeof(Animator))]
	public class AnimationAutoDestroy : MonoBehaviour
	{

		[SerializeField] private float delay = 0f;

		void Awake() {
			Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
		}
	}
}
