using UnityEngine;
using System.Collections;

public class OnTouchEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    public bool isKillEffect;
    public bool isDrunkEffect;
    public bool isConfusionEffect;
    public bool isPukeEffect;
    public bool isSpeedEffect;
    public bool shouldResetEffects;
    public float speedModifier;
    public PlayerControl2D.Objective.Type objectiveType;
    
	// Update is called once per frame
	void Update () {
	}

    bool SetObstacleEnabled(Transform t, bool isEnabled)
    {
        OnTouchEffect te = t.GetComponent<OnTouchEffect>();
        if (te)
            te.enabled = isEnabled;
        MovableEnemyStart me = t.GetComponent<MovableEnemyStart>();
        if (me)
            me.enabled = isEnabled;
        PeriodicEnemy pe = t.GetComponent<PeriodicEnemy>();
        if (pe)
            pe.enabled = isEnabled;
        BoxCollider2D bc = t.GetComponent<BoxCollider2D>();
        if (bc)
            bc.enabled = isEnabled;
        return (te || me || pe || bc);
    }

    public void SetActive(bool isActive)
    {
        var validChildren = new System.Collections.Generic.List<Transform>();
        var children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            Transform child = transform.GetChild(i);
            if (SetObstacleEnabled(child, false))
                validChildren.Add(child);
        }
        if (validChildren.Count > 0)
        {
            SetObstacleEnabled(validChildren[Random.Range(0, validChildren.Count)], true);
        }
    }
}
