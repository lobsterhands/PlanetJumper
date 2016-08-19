using UnityEngine;
using System.Collections;

public class PlanetSettings : MonoBehaviour {

	public float mass; // make this public and have unique mass for each planet?? How does unity values override script?

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
