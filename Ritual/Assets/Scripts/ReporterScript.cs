using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReporterScript : MonoBehaviour {

	private AudioSource audio;
	private GameObject uiCanvas;

	public GameObject textPrefab;
	public string promptText;

	public float triggerRate = 5.0f;
	private float nextTrigger;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		uiCanvas = GameObject.FindWithTag ("UI/Canvas");
	}

	void Yell() {
		audio.Play ();
	}

	void ReportText() {
		if (promptText.Equals (string.Empty)) {
			return;
		}
		GameObject prompt = Instantiate (textPrefab);
		prompt.transform.FindChild ("Text").GetComponent<Text> ().text = promptText;
		prompt.transform.SetParent (uiCanvas.transform);
		prompt.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-200.0f, 50.0f);
		StartCoroutine (RemovePrompt (5, prompt));

        ReporterScript rs = this.GetComponent<ReporterScript>();
        if (rs)
            rs.enabled = false;
    }

	IEnumerator RemovePrompt(int sec, GameObject prompt) {
		yield return new WaitForSeconds (sec);
		prompt.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other) {
        if (!enabled)
            return;

		if (other.gameObject.tag == "Player") {
			if (Time.time > nextTrigger) {
				nextTrigger = Time.time + triggerRate;
				ReportText ();
				Yell ();
			}
		}
	}
	
}