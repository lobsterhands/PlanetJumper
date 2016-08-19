using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	const float PLAYER_MASS = 1f;
	float speed = 10;
	float jumpForce = 1000f;

	public List<GameObject> planets;

	GameObject currentPlanet;

	Rigidbody RB;

	CalculateDirection calcDir_Script;
	Vector3 jumpDir;

	bool isJumping = true;

	// Use this for initialization
	void Start () {
		RB = this.gameObject.GetComponent<Rigidbody> ();
		QualitySettings.vSyncCount = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentPlanet != null) {
			var x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
			transform.Translate(-x, 0, 0); // Unsure why -x is necessary, but it currently is

			if (!isJumping) {
				if (Input.GetButtonDown ("Jump")) {
					jumpDir = calcDirPlayer (currentPlanet.transform.position, this.gameObject.transform.position);
					RB.AddForce (jumpDir * jumpForce);
					isJumping = true;
				}
			}
		}
	}

	// FixedUpdate
	float maxForceGravity;
	float acceleration;
	Vector3 directionGravity;
	void FixedUpdate()
	{
		maxForceGravity = 0;
		foreach (GameObject planet in planets) {
			float forceGravity = CalcForce (planet, this.gameObject);
			if ( forceGravity >= maxForceGravity) {
				maxForceGravity = forceGravity;
				acceleration = maxForceGravity / PLAYER_MASS;
				directionGravity = calcDirForce (planet.transform.position, this.transform.position);
				RB.AddForce (directionGravity * acceleration);
				RB.transform.rotation = Quaternion.FromToRotation (Vector3.up, (planet.transform.position - this.transform.position).normalized);
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		isJumping = false;
		currentPlanet = collision.gameObject;
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

	public float CalcForce(GameObject planet, GameObject player)
	{
		PlanetSettings planet_script = planet.GetComponent<PlanetSettings> ();

		Vector3 planetPos = planet.transform.position;
		Vector3 playerPos = player.transform.position;
		float distance = (planetPos - playerPos).magnitude;
		float force = ((planet_script.getMass() * PLAYER_MASS) / (distance * distance));
		return force;
	}
}
