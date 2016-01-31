using UnityEngine;
using System.Collections;

public class MovableEnemyStart : MonoBehaviour {

    public float spawnInterval;
    public float timeToReachTarget;
    public GameObject enemyGameObject;
    public GameObject endPosition;
    public float t;

	// Use this for initialization
	void Start () {
        t = spawnInterval;
    }
	
	// Update is called once per frame
	void Update () {
        t -= Time.deltaTime;
	    if (t <= 0.0f)
        {
            t = spawnInterval;
            SpawnEnemy();
        }
	}

    void SpawnEnemy()
    {
        GameObject enemy = (GameObject)Instantiate(enemyGameObject, gameObject.transform.position, enemyGameObject.transform.rotation);
        enemy.GetComponent<MovableEnemy>().startPosition = gameObject;
        enemy.GetComponent<MovableEnemy>().endPosition = endPosition;
        enemy.GetComponent<MovableEnemy>().timeToReachTarget = timeToReachTarget;        
    }
}
