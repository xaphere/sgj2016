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
                    levelTimeleft -= Time.deltaTime;
                    GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("countdown").GetComponent<UnityEngine.UI.Text>().text = Mathf.Ceil(levelTimeleft).ToString();
                    if (levelTimeleft <= 0.0f)
                    {
                        if (player.GetComponent<PlayerControl2D>().isObjectiveDone())
                        {
                            ToNextLevel();
                        }
                        else
                        {
                            ToGameOverState();
                        }
                    }
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

        var list = new System.Collections.Generic.List<PlayerControl2D.Objective.Type>{
            PlayerControl2D.Objective.Type.breakfast,
            PlayerControl2D.Objective.Type.break_glass,
            PlayerControl2D.Objective.Type.alcohol,
            PlayerControl2D.Objective.Type.smoke,
            PlayerControl2D.Objective.Type.milk,
            PlayerControl2D.Objective.Type.selfie,
            PlayerControl2D.Objective.Type.brothel,
            PlayerControl2D.Objective.Type.medicine,
            PlayerControl2D.Objective.Type.workout,
            PlayerControl2D.Objective.Type.gamble,
            PlayerControl2D.Objective.Type.zoo,
            PlayerControl2D.Objective.Type.piss,
            PlayerControl2D.Objective.Type.hair,
        };
       
        int n = list.Count;
        Random rng = new Random();
        for (int i = 0; i < n; i++)
        {
            var value = list[i];
            int r = Random.Range(i, n);
            list[i] = list[r];
            list[r] = value;
        }

        var activation = new System.Collections.Generic.Dictionary<PlayerControl2D.Objective.Type, bool>();
        var activeList = new System.Collections.Generic.List<PlayerControl2D.Objective.Type>();
        for (int i = 0; i < n; i++)
        {
            if (i < 3)
            {
                activation[list[i]] = true;
                activeList.Add(list[i]);
            }
            else
            {
                activation[list[i]] = false;
            }
        }

        foreach (var ob in GameObject.FindGameObjectsWithTag("Objective"))
        {
            var te = ob.GetComponent<OnTouchEffect>();
            if (te)
            {
                if (activation.ContainsKey(te.objectiveType))
                    te.SetActive(activation[te.objectiveType]);
                else
                    te.SetActive(true);
            }
        }


        player.GetComponent<PlayerControl2D>().SetUpObjectives(activeList);
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

    public void ToNextLevel()
    {
        ResetPlayer();
        ToPrepareState();
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
