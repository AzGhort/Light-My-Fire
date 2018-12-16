using UnityEngine;

namespace LightMyFire
{
	public class GameStateInit : MonoBehaviour
	{
		private void Awake() {
			GameState.ResetGameProgress();
		}
	}
}