using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public void StartGame() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		StartCoroutine (SwitchScenes());
	}

	IEnumerator SwitchScenes() {
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene ("Scenes/Ivan");
	}

	public void Credits() {
		gameObject.transform.FindChild ("CreditsPanel").gameObject.SetActive (true);
	}

	public void BackToMain() {
		gameObject.transform.FindChild ("CreditsPanel").gameObject.SetActive (false);
	}
}
