using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightMyFire
{
	public class LevelChangerSingleton : Singleton<LevelChangerSingleton>
	{
		private static Animator animator = null;
		private static SceneField sceneToLoad = null;

		// Animation events cant use static methods... 
		public void AnimationOnSceneLoadComplete() {
			OnLoadSceneComplete();
		}

		public void AnimationStopInput() {
			Debug.Log("LevelChanger - Stopped other input");
			GameState.PlayerFrozen = true;
			PauseMenu.PauseProhibited = true;
		}

		public void AnimationAllowInput() {
			Debug.Log("LevelChanger - Allowed other input");
			GameState.PlayerFrozen = false;
			PauseMenu.PauseProhibited = false;
		}

		public static void LoadScene(SceneField scene) {
			animator.SetTrigger("FadeOut");
			sceneToLoad = scene;
		}

		public static void OnLoadSceneComplete() {
			//StartCoroutine("coOnFadeComplete");
			SceneManager.LoadScene(sceneToLoad);
		}

		public static void FadeIn() {
			animator.SetTrigger("FadeIn");
		}

		public static void FadeOut() {
			animator.SetTrigger("FadeOut");
		}

		// Makes sure that object will live only as singleton
		protected LevelChangerSingleton() { }

		private void Awake() {
			Debug.Log("Awoke Singleton Instance: " + gameObject.GetInstanceID());

			animator = Instance.GetComponent<Animator>();
			Debug.Assert(animator);

			SceneManager.sceneLoaded += onLevelFinishedLoading;
		}
		
		private static void onLevelFinishedLoading(Scene scene, LoadSceneMode mode) { FadeIn(); }

		// Async loading for bigger scenes if necessary

		private static AsyncOperation aop = null;

		private static IEnumerator coOnFadeComplete() {
			while (aop.progress < 0.9f) {
				yield return null;
			}
			aop.allowSceneActivation = true;
		}

		private static IEnumerator coLoadScene() {
			aop = SceneManager.LoadSceneAsync(sceneToLoad);
			aop.allowSceneActivation = false;
			yield return aop;
		}
	}
}