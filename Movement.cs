using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	float speed = 10;

	GameObject planet_GO;
	PlanetSettings planet_Script;
	Vector3 planetPos;

	Rigidbody RB;

	CalculateDirection calcDir_Script;
	Vector3 jumpDir;

	bool isJumping = false;

	// Use this for initialization
	void Start () {
		planet_GO = GameObject.FindGameObjectWithTag("Planet");
		planet_Script = planet_GO.GetComponent<PlanetSettings> ();
		planetPos = planet_Script.getLocation ();

		RB = this.GetComponent<Rigidbody> ();

		QualitySettings.vSyncCount = 0;

		calcDir_Script = this.gameObject.GetComponent<CalculateDirection> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		if (planetPos.y >= 0) {
			transform.Translate (x, 0, 0);
		} else {
			transform.Translate (-x, 0, 0);
		}

		if (!isJumping) {
			if (Input.GetButtonDown ("Jump")) {
				isJumping = true;
				jumpDir = calcDir_Script.calcDirPlayer (planetPos, this.gameObject.transform.position);
				RB.AddForce (jumpDir * 1300);
			}
		}

	}

	void OnCollisionEnter(Collision collision) {
		isJumping = false;
	}
}
