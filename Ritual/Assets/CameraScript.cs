using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        startingOffset = transform.position - player.transform.position;

    }

    public GameObject player;
    public float followBounds;
    Vector3 startingOffset;
	// Update is called once per frame
	void Update () {
        //if ((transform.position - startingOffset - player.transform.position).magnitude > followBounds)
        { 
            Vector3 newPos = Vector3.Lerp(transform.position - startingOffset, player.transform.position, 0.1f);
            transform.position = newPos + startingOffset;
        }
    }
}
