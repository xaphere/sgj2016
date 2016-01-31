using UnityEngine;
using System.Collections;

public class scaleOverT : MonoBehaviour {

	// Use this for initialization
	void Start () {
        startScale = transform.localScale;
    }

    public float time;
    public float scaleMultiplier;
    Vector3 startScale;
    float t;

	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        transform.localScale = startScale * Mathf.Lerp(1.0f, scaleMultiplier, Mathf.Min(1.0f, t/ time));
    }
}
