using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	float speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;

		transform.Translate (-x, 0, 0);
	}
}
