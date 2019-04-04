using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController1 : MonoBehaviour {

    Rigidbody rigb;
    SphereCollider col;
    Component TriggerSphere;
	// Use this for initialization
	void Start () {
        TriggerSphere = GameObject.Find("TriggerSphere").GetComponent("TestAttachment1");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && TestAttachment1.InRange)
        {
            rigb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        }
	}
}
