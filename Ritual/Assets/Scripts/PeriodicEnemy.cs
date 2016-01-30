using UnityEngine;
using System.Collections;

public class PeriodicEnemy : MonoBehaviour {

    public float repeatRate;
    private BoxCollider collider;

	// Use this for initialization
	void Start () {
        InvokeRepeating("ShowPeriodically", 0, repeatRate);
        collider = gameObject.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ShowPeriodically()
    {
        collider.enabled = !collider.enabled;
    }
}
