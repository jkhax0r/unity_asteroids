﻿using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		// Slowly rotate the object around its X axis at 1 degree/second.
		transform.Rotate(Vector3.right * Time.deltaTime * 0.8f);
		// ... at the same time as spinning relative to the global 
		// Y axis at the same speed.
		transform.Rotate(Vector3.up * Time.deltaTime * 0.8f, Space.World);
	
	}
}
