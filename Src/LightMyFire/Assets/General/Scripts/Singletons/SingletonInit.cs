using UnityEngine;

namespace LightMyFire
{
	public class SingletonInit : MonoBehaviour
	{
		private void Awake() {
			var init = LevelChangerSingleton.Instance;
		}
	}
}