using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttachment1 : MonoBehaviour {

    SphereCollider col;
    GameObject PlayerSphere;
    public static bool InRange = false;
    public static int triggered = 0;
	// Use this for initialization
	void Start () {
        PlayerSphere = GameObject.Find("PlayerSphere");
	}
	
	// Update is called once per frame
	void Update () {
        if (triggered > 0)
            InRange = true;
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "PlayerSphere")
            triggered++;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "playerSphere")
            triggered--;
    }
}
