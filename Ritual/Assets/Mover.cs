using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public float speed;

	// Update is called once per frame
	void Update () {
        float x = -Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(x, 0.0f, z);
        Vector3 moveVec = dir * speed;

        Rigidbody rb = GetComponent<Rigidbody>();
        RaycastHit hitInfo;
        if (rb.SweepTest(dir, out hitInfo, moveVec.magnitude*2.0f))
        {
            //moveVec *= moveVec.magnitude / hitInfo.distance;
            moveVec *= 0.0f;
            print("AAAAA");
        }
        transform.position += moveVec;
    }
}
