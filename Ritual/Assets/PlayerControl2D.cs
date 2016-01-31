using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl2D : MonoBehaviour
{

    public RuntimeAnimatorController runLeft;
    public RuntimeAnimatorController runRight;
    public RuntimeAnimatorController idleLeft;
    public RuntimeAnimatorController idleRight;
    private bool turnedLeft = false;  //0 is right, 1 is left.

    // Use this for initialization
    void Start()
    {

    }
    public float speed = 300.0f;
    public float speedMultiplier = 1.0f;

    public float confusedInterval = 0.0f;

    public float pukeIntervalMin;
    public float pukeIntervalMax;
    public float pukeLength;

    public Texture2D drunk_texture;
    public Texture2D dizzy_texture;
    public Texture2D draifus_texture;

    public Texture2D o_breakfast;
    public Texture2D o_break_glass;
    public Texture2D o_alcohol;
    public Texture2D o_smoke;
    public Texture2D o_milk;
    public Texture2D o_selfie;
    public Texture2D o_brothel;
    public Texture2D o_medicine;
    public Texture2D o_workout;
    public Texture2D o_gamble;
    public Texture2D o_zoo;
    public Texture2D o_piss;
    public Texture2D o_hair;
    public Texture2D o_school;
    public Texture2D o_church;

    float confusedT = 0.0f;
    float confusedLerp = 0.0f;
    Vector2 confusedDir1;
    Vector2 confusedDir2;

    float drunkT = 0.0f;

    float pukeT = 0.0f;
    float pukeCycle = 0.0f;
    bool isPuking = false;

    public class Objective
    {
        public enum Type
        {
            none,
            breakfast,
            break_glass,
            alcohol,
            smoke,
            milk,
            selfie,
            brothel,
            medicine,
            workout,
            gamble,
            zoo,
            piss,
            hair,
            sect,
            school
        }

        public Type objectiveType;
        public bool isDone;
    }


    System.Collections.Generic.List<Objective> objectives = new System.Collections.Generic.List<Objective>();

    public System.Collections.Generic.List<Objective> GetObjectives()
    {
        return objectives;
    }

    public void SetUpObjectives(System.Collections.Generic.List<Objective.Type> list)
    {
        objectives.Clear();        

        for (int i = 0; i < Mathf.Min(list.Count, 3); ++i)
        {
            Objective ob = new Objective();
            ob.objectiveType = list[i];
            ob.isDone = false;
            objectives.Add(ob);
            print(ob.objectiveType.ToString());

            GameObject check = GameObject.Find("Canvas/objective" + (i + 1).ToString() + "/check" + (i + 1).ToString());
            RawImage cri = check.GetComponent<RawImage>();
            cri.enabled = false;

            GameObject objective = GameObject.Find("Canvas/objective" + (i + 1).ToString());
            RawImage ri = objective.GetComponent<RawImage>();

            Texture2D objective_texture = new Texture2D(1, 1);

            switch (ob.objectiveType)
            {
                case Objective.Type.breakfast:
                    objective_texture = o_breakfast;
                    break;
                case Objective.Type.break_glass:
                    objective_texture = o_break_glass;
                    break;
                case Objective.Type.alcohol:
                    objective_texture = o_alcohol;
                    break;
                case Objective.Type.smoke:
                    objective_texture = o_smoke;
                    break;
                case Objective.Type.milk:
                    objective_texture = o_milk;
                    break;
                case Objective.Type.selfie:
                    objective_texture = o_selfie;
                    break;
                case Objective.Type.brothel:
                    objective_texture = o_brothel;
                    break;
                case Objective.Type.medicine:
                    objective_texture = o_medicine;
                    break;
                case Objective.Type.workout:
                    objective_texture = o_workout;
                    break;
                case Objective.Type.gamble:
                    objective_texture = o_gamble;
                    break;
                case Objective.Type.zoo:
                    objective_texture = o_zoo;
                    break;
                case Objective.Type.piss:
                    objective_texture = o_piss;
                    break;
                case Objective.Type.hair:
                    objective_texture = o_hair;
                    break;
                case Objective.Type.school:
                    objective_texture = o_school;
                    break;
                case Objective.Type.sect:
                    objective_texture = o_church;
                    break;
            }
            ri.texture = objective_texture;

        }

        {
            Objective ob = new Objective();
            ob.objectiveType = Objective.Type.school;
            ob.isDone = false;
            objectives.Add(ob);
        }
    }

    public void CompleteObjective(Objective.Type obType)
    {
        for (int i = 0; i < objectives.Count; ++i)
        {
            if (!objectives[i].isDone && objectives[i].objectiveType == obType)
            {
                objectives[i].isDone = true;
                if (i < 3)
                {
                    GameObject check = GameObject.Find("Canvas/objective" + (i + 1).ToString() + "/check" + (i + 1).ToString());
                    RawImage cri = check.GetComponent<RawImage>();
                    cri.enabled = true;
                }
            }
        }
        if (isObjectiveDone() && obType == Objective.Type.school)
        {
            GameObject.FindGameObjectWithTag("GM").GetComponent<GameplayManager>().ToNextLevel();
        }
    }

    public bool isObjectiveDone()
    {
        foreach (Objective ob in objectives)
        {
            if (!ob.isDone && ob.objectiveType != Objective.Type.none)
                return false;
        }
        return true;
    }

    public void MakeDrunk(float t)
    {
        drunkT = t;
    }

    public void MakeConfused(float t)
    {
        confusedT = t;
        confusedDir1 = Random.insideUnitCircle;
        confusedDir2 = Random.insideUnitCircle;
    }

    public void MakePuke(float t)
    {
        pukeT = t;
    }

    public void SetSpeedMultiplier(float v)
    {
        speedMultiplier = v;
    }

    public void ResetModifiers()
    {
        drunkT = 0.0f;
        confusedT = 0.0f;
        pukeT = 0.0f;
        speedMultiplier = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //print(x);
        //print(y);
        if (x > 0.01f) x = 1.0f;
        if (x < -0.01f) x = -1.0f;
        if (y > 0.01f) y = 1.0f;
        if (y < -0.01f) y = -1.0f;
        if (Input.GetButtonDown("Fire1")) { MakeConfused(2.0f); }
        if (Input.GetButtonDown("Fire2")) { MakeDrunk(2.0f); }
        if (Input.GetButtonDown("Fire3")) { MakePuke(2.0f); }
        if (Input.GetButtonDown("Jump")) { ResetModifiers(); }


        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        GameObject status = GameObject.Find("Canvas/status");
        RawImage ri = status.GetComponent<RawImage>();
        ri.enabled = false;

        if (confusedT > 0.0f)
        {
            ri.enabled = true;
            ri.texture = dizzy_texture;
            //status.SetActive (true);
            confusedT -= Time.deltaTime;

            if (confusedLerp <= 0.0f)
            {
                confusedDir1 = confusedDir2;
                confusedDir2 = Random.insideUnitCircle;
                confusedLerp = confusedInterval;
            }
            //xBias = Mathf.Lerp(confusedDir1.x, confusedDir2.x, confusedLerp);
            //yBias = Mathf.Lerp(confusedDir1.y, confusedDir2.y, confusedLerp);
            if (Mathf.Abs(x) > 0f || Mathf.Abs(y) > 0f)
            {
                confusedLerp -= Time.deltaTime;
                //x += confusedDir1.x;
                //y += confusedDir1.y;
                x += Mathf.Lerp(confusedDir1.x, confusedDir2.x, confusedLerp);
                y += Mathf.Lerp(confusedDir1.y, confusedDir2.y, confusedLerp);
            }
            Debug.DrawLine(transform.position, transform.position + new Vector3(confusedDir1.x, confusedDir1.y, 0f));


        }

        if (drunkT > 0.0f)
        {
            ri.enabled = true;
            ri.texture = drunk_texture;

            drunkT -= Time.deltaTime;
            x *= -1;
            y *= -1;
        }

        if (pukeT > 0.0f)
        {
            ri.enabled = true;
            ri.texture = draifus_texture;

            pukeCycle -= Time.deltaTime;
            if (pukeCycle <= 0.0f)
            {
                isPuking = !isPuking;
                pukeCycle = isPuking ? pukeLength : Random.Range(pukeIntervalMin, pukeIntervalMax);
            }
            if (isPuking)
            {
                x *= 0.0f;
                y *= 0.0f;
            }
        }

        Vector2 v = new Vector2(x, y).normalized * speed * speedMultiplier * Time.deltaTime;
        Animator animator = GameObject.Find("PlayerSprite").GetComponent<Animator>();
        if (v.magnitude == 0)
        {
            if (turnedLeft == false)
            {
                animator.runtimeAnimatorController = idleRight;
            }
            else
            {
                animator.runtimeAnimatorController = idleLeft;
            }
        }
        else
        {
            if (x > 0)
            {
                turnedLeft = false;
                animator.runtimeAnimatorController = runRight;
            }
            if (x < 0)
            {
                turnedLeft = true;
                animator.runtimeAnimatorController = runLeft;
            }
            if (y != 0)
            {
                if (turnedLeft == false)
                {
                    animator.runtimeAnimatorController = runRight;
                }
                else
                {
                    animator.runtimeAnimatorController = runLeft;
                }
            }
        }
        rb.velocity = v;
    }
}
