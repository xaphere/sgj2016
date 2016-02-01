using UnityEngine;
using System.Collections;

public class KillPlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            print("Game over!");
            //Destroy(gameObject);
        }
    }

}
