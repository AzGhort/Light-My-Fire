using System.Collections;

using UnityEngine;

namespace LightMyFire
{
    public class MusicPlayerSingleton : Singleton<MusicPlayerSingleton>
    {
        public enum MusicType
        {
            None, Menu, Casual, Battle
        }

        [SerializeField] private AudioClip menuClip;
        [SerializeField] private AudioClip casualClip;
        [SerializeField] private AudioClip battleClip;

        private static AudioSource audioSource = null;
        private static MusicType currentType = MusicType.None;

        public void HandleLoadedScene(MusicType sceneMusicType) {
            if (sceneMusicType == currentType) { return; }
            audioSource.Stop();

            if (sceneMusicType == MusicType.Menu) {
                audioSource.clip = menuClip;
            }
            else if (sceneMusicType == MusicType.Casual) { audioSource.clip = casualClip; }
            else if (sceneMusicType == MusicType.Battle) { audioSource.clip = battleClip; }
            
            currentType = sceneMusicType;
            StartCoroutine(fadeIntoSong());
        }

        public void FadeOutOfSong() {
            StartCoroutine(fadeOutOfSong());
        }

        // Makes sure that object will live only as singleton
        protected MusicPlayerSingleton() { }

        private void Awake() {
            Debug.Log("Awoke Singleton Instance: " + gameObject.GetInstanceID());

            audioSource = Instance.GetComponent<AudioSource>();
            Debug.Assert(audioSource);
        }

        private static IEnumerator fadeIntoSong() {
            audioSource.volume = 0;
            audioSource.Play();
            while (audioSource.volume < 0.5) {
                yield return new WaitForSeconds(0.1f);
                audioSource.volume += 0.01f;
            }
        }

        private static IEnumerator fadeOutOfSong() {
            Debug.Log("FadeOutSongStartCoroutine");

            while (audioSource.volume > 0) {
                audioSource.volume -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }

            Debug.Log("FinishFadeOutSongCor");
        }

    }
}