using UnityEngine;
using System.Collections;

public class deleateAfterT : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, afterT);
    }
    
    public float afterT;

    // Update is called once per frame
    void Update() {
    }
}
