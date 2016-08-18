using UnityEngine;
using System.Collections;

public class CalculateForce : MonoBehaviour {

	const float gravity = 100f;

	public float CalcForce(float massA, float massB, float distanceSquared) 
	{
		// F = ma = (gravity * m1 * m2) / r^2);
		float force = ((gravity * massA * massB) / distanceSquared);
		return force;
	}
}
