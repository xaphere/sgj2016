using UnityEngine;
using System.Collections;

public class PeriodicEnemy : MonoBehaviour {

    public float repeatRate;
    private BoxCollider collider;
    private Renderer renderer;

	// Use this for initialization
	void Start () {
        InvokeRepeating("ShowPeriodically", 0, repeatRate);
        collider = gameObject.GetComponent<BoxCollider>();
        renderer = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (collider.enabled) {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.green;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        killPlayer(col);
    }

    void OnTriggerStay(Collider col)
    {
        killPlayer(col);
    }

    void killPlayer(Collider col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            print("Game over!");
            //Destroy(gameObject);
        }
    }

    void ShowPeriodically()
    {
        collider.enabled = !collider.enabled;
    }

}
