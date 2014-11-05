using UnityEngine;
using System.Collections;

public class AsteroidForces : MonoBehaviour {

	Vector3 randDirection;
	float SCALE = 10.0f;

	// Use this for initialization
	void Start () {
		float randX = Random.Range (-10.0f, 10.0f);
		float randY = Random.Range (-10.0f, 10.0f);
		float randZ = Random.Range (-10.0f, 10.0f);
		randDirection = new Vector3 (randX, randY, randZ);
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody.AddForce (randDirection * 1.0f);
	
	}
}
