using UnityEngine;
using System.Collections;

public class ObjectSettings : MonoBehaviour {

	float mass = 2; //kg?
	float speed = 10;

	public Vector3 getLocation() 
	{
		Vector3 objVector = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		return objVector;
	}

	public float getMass()
	{
		return mass;
	}

	public float getSpeed()
	{
		return speed;
	}
}
