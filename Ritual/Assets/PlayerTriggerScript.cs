using UnityEngine;
using System.Collections;

public class PlayerTriggerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        ResolveTrigger(col, false);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        ResolveTrigger(col, false);
    }

    void OnCollisionEnter2D(Collider2D col)
    {
        ResolveTrigger(col, true);
    }

    void OnCollisionStay2D(Collider2D col)
    {
        ResolveTrigger(col, true);
    }

    void ResolveTrigger(Collider2D col, bool isTrigger)
    {
        print("ResolveTrigger");
        OnTouchEffect touchEffect = col.GetComponent<OnTouchEffect>();
        if (!touchEffect)
            return;

        PlayerControl2D playerController = GetComponent<PlayerControl2D>();

        if (touchEffect.shouldResetEffects)
            playerController.ResetModifiers();
        if (touchEffect.isConfusionEffect)
            playerController.MakeConfused(180.0f);
        if (touchEffect.isDrunkEffect)
            playerController.MakeDrunk(180.0f);
        if (touchEffect.isPukeEffect)
            playerController.MakePuke(180.0f);
        if (touchEffect.isSpeedEffect)
            playerController.SetSpeedMultiplier(touchEffect.speedModifier);
        if (touchEffect.isKillEffect)
            GameObject.FindGameObjectWithTag("GM").GetComponent<GameplayManager>().ResetGame();
    }
}
