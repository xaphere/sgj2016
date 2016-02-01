using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public float speed;
    public float maxspeed;
    // Update is called once per frame
    void Update() {
        float x = -Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(x, 0.0f, z).normalized;
        Vector3 moveVec = dir * speed;
        
        Rigidbody rb = GetComponent<Rigidbody>();
        RaycastHit hitInfo;

        if (rb.SweepTest(new Vector3(moveVec.x, 0.0f, moveVec.z), out hitInfo, moveVec.magnitude))
        {
            /*
            if (rb.SweepTest(new Vector3(moveVec.x, 0.0f, 0.0f), out hitInfo, moveVec.magnitude))
            {
                print("A");
                moveVec.x *= 0.0f;
                moveVec.z *= 0.5f;
            }
            if (rb.SweepTest(new Vector3(0.0f, 0.0f, moveVec.z), out hitInfo, moveVec.magnitude))
            {
                print("B");
                print(hitInfo.distance);
                moveVec.x *= 0.5f;
                moveVec.z *= 0.0f;
            }
            */
            print("A");
            print(-hitInfo.normal * Vector3.Dot(hitInfo.normal, new Vector3(moveVec.x, 0.0f, moveVec.z).normalized) * speed);
            moveVec += -hitInfo.normal * Vector3.Dot(hitInfo.normal, new Vector3(moveVec.x, 0.0f, moveVec.z).normalized) * speed;
            moveVec *= 0.5f;
        }
        if (rb.SweepTest(new Vector3(moveVec.x, 0.0f, moveVec.z), out hitInfo, Mathf.Min(0.1f, moveVec.magnitude * 2.0f)))
        {
            moveVec.x *= 0.0f;
            moveVec.z *= 0.0f;
        }

        if (moveVec.magnitude < 0.01)
            moveVec *= 0.0f;
        transform.Translate(moveVec);

    }
}
