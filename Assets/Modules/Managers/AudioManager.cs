using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
	public class AudioManager : MonoBehaviour {
		public AudioClip bgm;
		AudioSource bgmSource, effectSource;

		public void PlayAudioEffect(AudioClip audio) {
			if(audio)
				effectSource.PlayOneShot(audio);
		}

		void Start() {
			var camera = Camera.main;

			bgmSource = camera.gameObject.AddComponent<AudioSource>();
			effectSource = camera.gameObject.AddComponent<AudioSource>();

			bgmSource.loop = true;
			if(SceneManager.GetActiveScene().name == ActLoader.mainSceneName && ActLoader.actToLoad) {
				bgmSource.clip = ActLoader.actToLoad.bgm;
				bgmSource.Play();
			}
			else if(bgm) {
				bgmSource.clip = bgm;
				bgmSource.Play();
			}

			effectSource.loop = false;
		}
	}
}