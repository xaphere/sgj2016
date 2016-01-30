using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReporterScript : MonoBehaviour {

	private AudioSource audio;
	private GameObject uiCanvas;

	public GameObject textPrefab;
	public string promptText;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		uiCanvas = GameObject.FindWithTag ("UI/Canvas");
		ReportText ();
		Yell ();
	}

	void Yell() {
		audio.Play ();
	}

	void ReportText() {
		GameObject prompt = Instantiate (textPrefab);
		prompt.transform.FindChild ("Text").GetComponent<Text> ().text = promptText;
		prompt.transform.SetParent (uiCanvas.transform);
		prompt.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-200.0f, 50.0f);
		StartCoroutine (RemovePrompt (3, prompt));
	}

	IEnumerator RemovePrompt(int sec, GameObject prompt) {
		yield return new WaitForSeconds (sec);
		prompt.SetActive (false);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			ReportText ();
			Yell ();
		}
	}
	
}