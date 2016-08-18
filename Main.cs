using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	CalculateForce calcForce_Script;
	CalculateDistance calcDist_Script;
	CalculateDirection calcDir_Script;

	GameObject planet_GO;
	PlanetSettings planet_Script;
	Vector3 planetPos;

	GameObject object_GO;
	ObjectSettings object_Script;
	Rigidbody object_RB;

	// Used in Update
	Vector3 objectPos;
	float distance;
	float force;
	Vector3 directionGravity;
	float accelerationDueGravity;

	// Use this for initialization
	void Start () {
		planet_GO = GameObject.FindGameObjectWithTag("Planet");
		planet_Script = planet_GO.GetComponent<PlanetSettings> ();
		planetPos = planet_Script.getLocation ();

		object_GO = GameObject.FindGameObjectWithTag ("Object");
		object_Script = object_GO.GetComponent<ObjectSettings> ();
		object_RB = object_GO.GetComponent<Rigidbody> ();

		calcDist_Script = this.GetComponent<CalculateDistance> ();
		calcForce_Script = this.GetComponent<CalculateForce> ();

		calcDir_Script = this.GetComponent<CalculateDirection> ();

		QualitySettings.vSyncCount = 0;
	}


	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		objectPos = object_Script.getLocation ();
		distance = calcDist_Script.CalcDistance (planetPos, objectPos);
		force = calcForce_Script.CalcForce (planet_Script.getMass (), object_Script.getMass (), distance * distance);
		directionGravity = calcDir_Script.calcDirForce (planetPos, objectPos);
		accelerationDueGravity = force / object_Script.getMass();
		directionGravity.x *= accelerationDueGravity;
		directionGravity.y *= accelerationDueGravity;
		object_RB.AddForce (directionGravity);
		object_RB.transform.rotation = Quaternion.FromToRotation (planetPos.normalized, (objectPos - planetPos).normalized);
	}
}
