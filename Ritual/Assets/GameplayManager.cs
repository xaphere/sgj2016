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

    private bool is_next_leveling = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("days").GetComponent<UnityEngine.UI.Text>().text = "Day: " + (currentLevel+1).ToString();

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
                    if (levelTimeleft <= 0.0f && !is_next_leveling)
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
        player.GetComponent<PlayerControl2D>().ResetModifiers();
    }

    void ToPrepareState()
    {
        timeToStart = prepareTime + 2;
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
            PlayerControl2D.Objective.Type.sect
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
            if (i < 15)
            //if (i < 3)
            {
                activation[list[i]] = true;
                activeList.Add(list[i]);
            }
            else
            {
                activation[list[i]] = false;
            }
        }
        //activation[PlayerControl2D.Objective.Type.breakfast] = true;
        //activeList.Add(PlayerControl2D.Objective.Type.breakfast);

        foreach (var ob in GameObject.FindGameObjectsWithTag("Objective"))
        {
            var te = ob.GetComponent<OnTouchEffect>();
            if (te)
            {
                if (activation.ContainsKey(te.objectiveType))
                    te.SetActive(activation[te.objectiveType]);
                else
                {
                    print("cccccc");
                    print(ob.name);
                    te.SetActive(true);
                }
                if (activation.ContainsKey(te.objectiveType))
                {
                    ReporterScript rs = ob.GetComponent<ReporterScript>();
                    if (rs)
                        rs.enabled = activation[te.objectiveType];
                }
                else
                {
                    ReporterScript rs = ob.GetComponent<ReporterScript>();
                    if (rs)
                        rs.enabled = true;
                }
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
        is_next_leveling = true;

        currentLevel += 1;

        StartCoroutine(MyCoroutine());

        ResetPlayer();
        ToPrepareState();
        
        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("days").GetComponent<UnityEngine.UI.Text>().text = "Day: " + (currentLevel + 1).ToString();
        is_next_leveling = false;
    }

     


 IEnumerator MyCoroutine()
{
        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("next").GetComponent<UnityEngine.UI.RawImage>().enabled = true;
        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("nextday").GetComponent<UnityEngine.UI.Text>().enabled = true;
        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("nextday").GetComponent<UnityEngine.UI.Text>().text = "DAY " + (currentLevel + 1).ToString() ;

        yield return new WaitForSeconds(2);

        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("next").GetComponent<UnityEngine.UI.RawImage>().enabled = false;
        GameObject.FindGameObjectWithTag("UI/Canvas").transform.FindChild("nextday").GetComponent<UnityEngine.UI.Text>().enabled = false;


    }

public void ResetGame()
    {
        print("Starting game");
        currentLevel = 0;
        StartCoroutine(MyCoroutine());
        ResetPlayer();
        ToPrepareState();
    }
    
}
