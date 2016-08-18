using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	CalculateForce calcForce_Script;
	CalculateDistance calcDist_Script;
	CalculateDirection calcDir_Script;

	GameObject planet_GO;
	PlanetSettings planet_Script;

	GameObject object_GO;
	ObjectSettings object_Script;
	Rigidbody object_RB;

	// Use this for initialization
	void Start () {
		planet_GO = GameObject.FindGameObjectWithTag("Planet");
		planet_Script = planet_GO.GetComponent<PlanetSettings> ();

		object_GO = GameObject.FindGameObjectWithTag ("Object");
		object_Script = object_GO.GetComponent<ObjectSettings> ();
		object_RB = object_GO.GetComponent<Rigidbody> ();

		calcDist_Script = this.GetComponent<CalculateDistance> ();
		calcForce_Script = this.GetComponent<CalculateForce> ();

		calcDir_Script = this.GetComponent<CalculateDirection> ();
	}

	// Update is called once per frame
	void Update () {
		Vector3 planetPos = planet_Script.getLocation ();
		Vector3 objectPos = object_Script.getLocation ();
		float distance = calcDist_Script.CalcDistance (planetPos, objectPos);
		float force = calcForce_Script.CalcForce (planet_Script.getMass (), object_Script.getMass (), distance * distance);
		Vector3 direction = calcDir_Script.calcDirForce (planetPos, objectPos);
		float acceleration = force / object_Script.getMass();
		direction.x *= acceleration;
		direction.y *= acceleration;
		direction.z *= acceleration;
		object_RB.AddForce (direction);
		object_RB.transform.rotation = Quaternion.FromToRotation (planetPos.normalized, (objectPos - planetPos).normalized);

	}
}
