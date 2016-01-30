using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ResetGame();
	}

    public Vector3 playerStart;

	// Update is called once per frame
	void Update () {
	
	}

    public void ResetGame()
    {
        print("Starting game");
        GameObject.FindGameObjectWithTag("Player").transform.position = playerStart;
    }
    
}
