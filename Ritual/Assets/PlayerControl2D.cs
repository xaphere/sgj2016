using UnityEngine;
using System.Collections;

public class PlayerControl2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public float speed = 300.0f;
    public float speedMultiplier = 1.0f;

    public float confusedInterval = 0.0f;

    public float pukeIntervalMin;
    public float pukeIntervalMax;
    public float pukeLength;

    float confusedT = 0.0f;
    float confusedLerp = 0.0f;
    Vector2 confusedDir1;
    Vector2 confusedDir2;

    float drunkT = 0.0f;

    float pukeT = 0.0f;
    float pukeCycle = 0.0f;
    bool isPuking = false;

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
        speedMultiplier = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        print(x);
        print(y);
        if (x > 0.01f) x = 1.0f;
        if (x < -0.01f) x = -1.0f;
        if (y > 0.01f) y = 1.0f;
        if (y < -0.01f) y = -1.0f;
        if (Input.GetButtonDown("Fire1")){ MakeConfused(2.0f); }
        if (Input.GetButtonDown("Fire2")) { MakeDrunk(2.0f); }
        if (Input.GetButtonDown("Fire3")) { MakePuke(2.0f); }
        if (Input.GetButtonDown("Jump")) { ResetModifiers(); }


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        

        if (confusedT > 0.0f)
        {
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
            drunkT -= Time.deltaTime;
            x *= -1;
            y *= -1;
        }

        if (pukeT > 0.0f)
        {
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

        Vector2 v = new Vector2(x, y).normalized * speed * Time.deltaTime;

        rb.velocity = v;
    }
}
