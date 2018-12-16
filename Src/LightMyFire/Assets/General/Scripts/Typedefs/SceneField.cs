using UnityEngine;
using Object = UnityEngine.Object;

// Allows direct assignment of Scene file into Inspector script field in Unity Editor

namespace LightMyFire
{
	[System.Serializable]
	public class SceneField
	{
		[SerializeField] private Object sceneAsset;
		[SerializeField] private string sceneName = "";

		public SceneField(string sceneName) {
			this.sceneName = sceneName;
		}

		public string SceneName {
			get { return sceneName; }
		}

		// Makes it work with the existing Unity methods (LoadLevel/LoadScene)
		public static implicit operator string(SceneField sceneField) {
			return sceneField.SceneName;
		}
	}
}