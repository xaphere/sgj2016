using UnityEngine;
using System.Collections;

public class MovableEnemy : MonoBehaviour {

    public float timeToReachTarget;
    public GameObject startPosition;
    public GameObject endPosition;    
    private float t;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (startPosition && endPosition)
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPosition.transform.position, endPosition.transform.position, t);

            if (Vector3.Distance(gameObject.transform.position, endPosition.transform.position) < 0.1)
            {
                Destroy(gameObject);
            }
        }
	}
}
