using UnityEngine;
using System.Collections;

public class CalculateDistance : MonoBehaviour {

	public float CalcDistance(Vector3 A, Vector3 B) 
	{
		float X = Mathf.Abs (A.x - B.x);
		float Y = Mathf.Abs (A.y - B.y);
		float Z = Mathf.Abs (A.z - B.z);

		return Mathf.Sqrt (X*X + Y*Y + Z*Z);
	}
}
