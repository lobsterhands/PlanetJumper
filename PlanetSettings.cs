using UnityEngine;
using System.Collections;

public class PlanetSettings : MonoBehaviour {

	float mass = 50;

	public Vector3 getLocation() 
	{
		Vector3 thisLoc = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		return thisLoc;
	}

	public float getMass()
	{
		return mass;
	}
}
