using UnityEngine;

namespace LightMyFire
{
	public class SingletonInit : MonoBehaviour
	{
		private void Awake() {
			var levelChanger = LevelChangerSingleton.Instance;
            var musicPlayer = MusicPlayerSingleton.Instance;
		}
	}
}