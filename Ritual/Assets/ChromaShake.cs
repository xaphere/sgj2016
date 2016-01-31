using UnityEngine;
using System.Collections;

public class ChromaShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    public float cycleTime;
    float t;
    public float a;
    public float b;
	// Update is called once per frame
	void Update () {
        t += cycleTime;
        GetComponent<UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration>().chromaticAberration = a + Mathf.Sin(t* cycleTime) * (b);
	}
}
