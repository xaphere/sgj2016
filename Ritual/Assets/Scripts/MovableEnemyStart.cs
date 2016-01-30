using UnityEngine;
using System.Collections;

public class MovableEnemyStart : MonoBehaviour {

    public float spawnInterval;
    public float timeToReachTarget;
    public GameObject enemyGameObject;
    public GameObject endPosition;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnEnemy()
    {
        GameObject enemy = (GameObject)Instantiate(enemyGameObject, gameObject.transform.position, gameObject.transform.rotation);
        enemy.GetComponent<MovableEnemy>().startPosition = gameObject;
        enemy.GetComponent<MovableEnemy>().endPosition = endPosition;
        enemy.GetComponent<MovableEnemy>().timeToReachTarget = timeToReachTarget;
    }
}
