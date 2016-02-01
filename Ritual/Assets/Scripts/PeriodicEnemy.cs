using UnityEngine;
using System.Collections;

public class PeriodicEnemy : MonoBehaviour {

    public float repeatRate;
    private BoxCollider2D collider;
    private Renderer renderer;

	// Use this for initialization
	void Start () {
        InvokeRepeating("ShowPeriodically", 0, repeatRate);
        collider = gameObject.GetComponent<BoxCollider2D>();
        renderer = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (collider.enabled) {
        //    renderer.material.color = Color.red;
        //}
        //else
        //{
        //    renderer.material.color = Color.green;
        //}
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        //killPlayer(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //killPlayer(col);
    }

    void killPlayer(Collider2D col)
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
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
        var te = GetComponent<OnTouchEffect>();
        if (te)
            te.enabled = !te.enabled;
    }

}
