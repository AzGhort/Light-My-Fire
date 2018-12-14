using UnityEngine;

// Source:
// http://www.bivis.com.br/2016/06/07/unity-creating-singleton-from-prefab/

namespace LightMyFire
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T instance;
		private static object mutex = new object();

		private static bool applicationIsQuitting = false;

		public static T Instance {
			get {
				if (applicationIsQuitting) {
					Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
						"' already destroyed on application quit." +
						" Won't create again - returning null.");
					return null;
				}

				lock (mutex) {
					if (instance == null) {
						instance = (T)FindObjectOfType(typeof(T));

						if (FindObjectsOfType(typeof(T)).Length > 1) {
							Debug.LogError("[Singleton] Something went really wrong " +
								" - there should never be more than 1 singleton!" +
								" Reopening the scene might fix it.");
							return instance;
						}

						if (instance == null) {
							GameObject singletonPrefab = null;
							GameObject singleton = null;

							// Check if exists a singleton prefab on Resources Folder.
							// -- Prefab must have the same name as the Singleton SubClass
							singletonPrefab = (GameObject)Resources.Load(typeof(T).Name.ToString(), typeof(GameObject));

							// Create singleton as new or from prefab
							if (singletonPrefab != null) {
								singleton = Instantiate(singletonPrefab);
								instance = singleton.GetComponent<T>();
							}
							else {
								singleton = new GameObject();
								instance = singleton.AddComponent<T>();
							}
							singleton.name = "(singleton) " + typeof(T).Name.ToString();
							DontDestroyOnLoad(singleton);
						}
						else {
							Debug.Log("[Singleton] Using instance already created: " +
								instance.gameObject.name);
						}
					}

					return instance;
				}
			}
		}

		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public void OnDestroy() {
			applicationIsQuitting = true;
		}
	}
}