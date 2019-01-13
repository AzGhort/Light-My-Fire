using UnityEngine;

namespace LightMyFire
{
    public class SideStreetScripts : MonoBehaviour
    {
        [SerializeField] private SceneField mainStreetScene;

		public void EnterMainStreet() {
			LevelChangerSingleton.LoadScene(mainStreetScene);
		}
    }
}