using UnityEngine;
using System.Collections;

public class CalculateDirection : MonoBehaviour {

	CalculateDistance calcDist_Script;


	// Use this for initialization
	void Start () {
		calcDist_Script = this.GetComponent<CalculateDistance> ();
	}

	public Vector3 calcDirForce(Vector3 A, Vector3 B)
	{
		float magnitude = calcDist_Script.CalcDistance (A, B);

		// Normalize
		float X = (A.x - B.x)/magnitude;
		float Y = (A.y - B.y)/magnitude;
		float Z = (A.z - B.z)/magnitude;

		Vector3 direction = new Vector3 (X, Y, Z);
		return direction;
	}

	public Vector3 calcDirPlayer(Vector3 A, Vector3 B)
	{
		float magnitude = calcDist_Script.CalcDistance (A, B);

		// Normalize
		float X = (A.x - B.x)/magnitude;
		float Y = (A.y - B.y)/magnitude;
		float Z = (A.z - B.z)/magnitude;

		Vector3 direction = new Vector3 (-X, -Y, -Z);
		return direction;
	}
}
