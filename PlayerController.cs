using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public int pointsEarned;
    public int TotalPoints;
    private bool IsGrounded;
    private bool IsWalled;
    private bool WallJump;
    GameObject[] PointPickUps;
    private Rigidbody rigb;
    private float JumpForce = 10f;
    public SphereCollider col;
    public Text PointsText;
    public Text winText;

    void Start ()
	{
        PointPickUps = GameObject.FindGameObjectsWithTag("Pick Up");
        TotalPoints = PointPickUps.Length;
        rigb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        pointsEarned = 0;
        SetPointsText();
        winText.text = "";
    }

    void FixedUpdate()
	{
        //calculates basic movement
        float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigb.AddForce (movement * speed);

        //this is for registering whether the player is very near ground or walls I.E grounded or walled
        RaycastHit hit;
        //the sphere cast is 50% bigger than the actual ball which has a radius of 0.5
        if (Physics.SphereCast(transform.position, 0.75f, Vector3.zero, out hit))
        {
            //debug raydrawing for spherecast trigger
            Debug.DrawRay(transform.position, transform.position + hit.barycentricCoordinate, Color.green, 1f);
            //if a raycast hit is between the bottom of the ball and 0.1 units below from the bottom of the ball, the ball is grounded
            if(hit.barycentricCoordinate.y < -0.5f && hit.barycentricCoordinate.y > -0.6f)
            {
                IsGrounded = true;
            }
            else IsGrounded = false;
            //if a raycast hit is adjacent horizontally or within 0.25 units horizontally from the ball, the ball is walled
            if(hit.barycentricCoordinate.x > 0.5f || hit.barycentricCoordinate.x < -0.5f || hit.barycentricCoordinate.z > 0.5f || hit.barycentricCoordinate.z < -0.5f)
            {
                IsWalled = true;
            }
            else IsWalled = false;
        }

        //for jumping from the ground
        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            Debug.Log("grounded");
        }
        //for jumping off walls
        if(IsWalled && !IsGrounded && WallJump && Input.GetKeyDown(KeyCode.Space))
        {
            //basically you reflectively bounce off the wall and your vertical speed is cancelled out to 0
            rigb.AddForce(new Vector3(-movement.x * 2, -movement.y, -movement.z * 2));
            rigb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            Debug.Log("walled");
            WallJump = false;
        }
        //walljump allows you to jump on walls but keeps you from basically levitating up walls by spamming space
        if (!IsWalled)
            WallJump = true;
    }
    //debug raydrawing for collision
    private void OnCollisionEnter(Collision hit)
    {
        foreach (ContactPoint contact in hit.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red, 0.5f);
        }
    }
    //if you collide with a pickup you pick it up
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            pointsEarned += 1;
            SetPointsText();
        }
    }
    //displays wintext when you collect all the points
    void SetPointsText()
    {
        PointsText.text = "Points:" + pointsEarned.ToString();
        if(pointsEarned == TotalPoints)
            winText.text = "You Win";
    }
}