using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour {

    public float prepareTime = 3f;
    public float levelTimeTotal = 10f;

    int currentLevel;
    public float levelTimeleft;
    public float timeToStart;

    public enum GameState
    {
        Prepare,
        Play,
        GameOver,
    }

    public GameState state;
    public Vector3 playerStart;
    public GameObject textPrefab;
    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ResetGame();
	}
    
    // Update is called once per frame
    void Update () {
	    switch(state)
        {
            case GameState.Prepare:
                {
                    timeToStart -= Time.deltaTime;
                    string txt = Mathf.Ceil(timeToStart).ToString();
                    if (timeToStart <= 0.0f)
                    {   
                        ToPlayState();
                        txt = "GO!";
                    }
                    GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("countdown").GetComponent<UnityEngine.UI.Text>().text = txt;
                }
                break;
            case GameState.Play:
                {
                    if (levelTimeleft <= 0.0f)
                    {
                        if (player.GetComponent<PlayerControl2D>().isObjectiveDone)
                        {
                            NextLevel();
                            ToPrepareState();
                        }
                        else
                        {   
                            ToGameOverState();
                        }
                    }
                    levelTimeleft -= Time.deltaTime;
                }
                break;
            case GameState.GameOver:
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        ResetGame();
                    }
                }
                break;
        }
	}

   

    public void ResetPlayer()
    {
        player.transform.position = playerStart;
    }

    void ToPrepareState()
    {
        timeToStart = prepareTime;
        player.GetComponent<PlayerControl2D>().SetSpeedMultiplier(0.0f);
        state = GameState.Prepare;
    }
    void ToPlayState()
    {
        levelTimeleft = levelTimeTotal;
        player.GetComponent<PlayerControl2D>().SetSpeedMultiplier(1.0f);
        state = GameState.Play;
    }

    public void ToGameOverState()
    {
        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("countdown").GetComponent<UnityEngine.UI.Text>().text = "GAME OVER";
        player.GetComponent<PlayerControl2D>().SetSpeedMultiplier(0.0f);
        state = GameState.GameOver;
    }

    void NextLevel()
    {
        currentLevel += 1;
    }

    public void ResetGame()
    {
        print("Starting game");
        ResetPlayer();
        ToPrepareState();
        currentLevel = 0;
    }
    
}
