using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	const float PLAYER_MASS = 2f;
	float speed = 10;
	const float GRAVITY = 100f;

	public List<GameObject> planets;

	GameObject planet_GO;
	PlanetSettings planet_Script;
	Vector3 planetPos;

	Rigidbody RB;

	CalculateDirection calcDir_Script;
	Vector3 jumpDir;

	bool isJumping = false;

	// Use this for initialization
	void Start () {
		planet_GO = GameObject.FindGameObjectWithTag("Planet");
		planet_Script = planet_GO.GetComponent<PlanetSettings> ();
		planetPos = planet_Script.getLocation ();

		RB = this.GetComponent<Rigidbody> ();

		QualitySettings.vSyncCount = 0;

		calcDir_Script = this.gameObject.GetComponent<CalculateDirection> ();

		foreach (GameObject x in planets) {
			Debug.Log (x.transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		if (planetPos.y >= 0) {
			transform.Translate (x, 0, 0);
		} else {
			transform.Translate (-x, 0, 0);
		}

		if (!isJumping) {
			if (Input.GetButtonDown ("Jump")) {
				isJumping = true;
				jumpDir = calcDir_Script.calcDirPlayer (planetPos, this.gameObject.transform.position);
				RB.AddForce (jumpDir * 1300);
			}
		}

	}

	float maxForce = 0;
	void FixedUpdate()
	{
		foreach (GameObject planet in planets) {

		}
//		distance = calcDist_Script.CalcDistance (planetPos, objectPos);
//		force = calcForce_Script.CalcForce (planet_Script.getMass (), object_Script.getMass (), distance * distance);
//		directionGravity = calcDir_Script.calcDirForce (planetPos, objectPos);
//		accelerationDueGravity = force / object_Script.getMass();
//
//		directionGravity.x *= accelerationDueGravity;
//		directionGravity.y *= accelerationDueGravity;
//		directionGravity.z = 0;
//
//		object_RB.AddForce (directionGravity);
//		object_RB.transform.rotation = Quaternion.FromToRotation (Vector3.up, (planetPos - objectPos).normalized);
	}

	void OnCollisionEnter(Collision collision) {
		isJumping = false;
	}

	Vector3 calcDirForce(Vector3 A, Vector3 B)
	{
		float magnitude = CalcDistance (A, B);

		// Normalize
		float X = (A.x - B.x)/magnitude;
		float Y = (A.y - B.y)/magnitude;
		float Z = (A.z - B.z)/magnitude;

		Vector3 direction = new Vector3 (X, Y, Z);
		return direction;
	}

	Vector3 calcDirPlayer(Vector3 A, Vector3 B)
	{
		float magnitude = CalcDistance (A, B);

		// Normalize
		float X = (A.x - B.x)/magnitude;
		float Y = (A.y - B.y)/magnitude;
		float Z = (A.z - B.z)/magnitude;

		Vector3 direction = new Vector3 (-X, -Y, -Z);
		return direction;
	}

	public float CalcDistance(Vector3 A, Vector3 B) 
	{
		float X = Mathf.Abs (A.x - B.x);
		float Y = Mathf.Abs (A.y - B.y);
		float Z = Mathf.Abs (A.z - B.z);

		return Mathf.Sqrt (X*X + Y*Y + Z*Z);
	}
		
	public float CalcForce(float massA, float massB, float distanceSquared) 
	{
		// F = ma = (gravity * m1 * m2) / r^2);
		float force = ((GRAVITY * massA * massB) / distanceSquared);
		return force;
	}

//	public float CalcForce(GameObject planet, GameObject player)
//	{
//		Vector3 planetPos = planet.transform.position;
//		Vector3 playerPos = player.transform.position;
//
//		float distance = (planetPos - playerPos).magnitude;
//		float force = ((GRAVITY * planet.getMass() * PLAYER_MASS) / distance * distance);
//	}
}
