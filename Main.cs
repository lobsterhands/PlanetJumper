using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	CalculateForce calcForce_Script;
	CalculateDistance calcDist_Script;
	CalculateDirection calcDir_Script;

	GameObject planet_GO;
	PlanetSettings planet_Script;
	Vector3 planetPos;

	GameObject planet_02_GO;
	PlanetSettings planet_02_Script;
	Vector3 planet_02_Pos;

	List<Vector3> planetPositions;
	List<float> fromPlayerToPlanetDistances;
	List<float> gravityForces;

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

		planet_02_GO = GameObject.FindGameObjectWithTag("Planet_02");
		planet_02_Script = planet_02_GO.GetComponent<PlanetSettings> ();
		planet_02_Pos = planet_02_Script.getLocation ();

		object_GO = GameObject.FindGameObjectWithTag ("Object");
		object_Script = object_GO.GetComponent<ObjectSettings> ();
		object_RB = object_GO.GetComponent<Rigidbody> ();


		calcDist_Script = this.GetComponent<CalculateDistance> ();
		calcForce_Script = this.GetComponent<CalculateForce> ();

		calcDir_Script = this.GetComponent<CalculateDirection> ();

		QualitySettings.vSyncCount = 0;

		planetPositions = new List<Vector3> ();
		planetPositions.Add (planetPos);
		planetPositions.Add (planet_02_Pos);

		fromPlayerToPlanetDistances = new List<float> ();
		gravityForces = new List<float> ();

		for (var i = 0; i < planetPositions.Count; i++) { // Have to initialize with length to set by index later
			fromPlayerToPlanetDistances.Add (0);
			gravityForces.Add (0);
		}


	}


	// Update is called once per frame
	void Update () {
		
	}

	// NOTE: LYLE! This is getting ugly. Idea: there should be one List which holds all the planet game objects.
	// On each update, the player body should calculate (distance and forces simultanously since that is what
	// the distance is for. Each planet should return only the force and direction due to gravity. Each force should
	// add/subtract up and provide a final force. (Though I think the real gravity will be activated / deactivated 
	// as the player leaves a planet for the next one. It would be nice to have actual active forces working though
	// at least for the learning experience and future use.

	// Calculate all forces; greatest force determines the player orientation

	void FixedUpdate()
	{
		objectPos = object_Script.getLocation ();

		for (int i = 0; i < planetPositions.Count; i++) {
			fromPlayerToPlanetDistances[i] = calcDist_Script.CalcDistance (planetPositions[i], objectPos);
		}
			
		distance = calcDist_Script.CalcDistance (planetPos, objectPos);
		force = calcForce_Script.CalcForce (planet_Script.getMass (), object_Script.getMass (), distance * distance);
		directionGravity = calcDir_Script.calcDirForce (planetPos, objectPos);
		accelerationDueGravity = force / object_Script.getMass();
		directionGravity.x *= accelerationDueGravity;
		directionGravity.y *= accelerationDueGravity;
		directionGravity.z = 0;
		object_RB.AddForce (directionGravity);
		object_RB.transform.rotation = Quaternion.FromToRotation (Vector3.up, (planetPos - objectPos).normalized);
	}
}
