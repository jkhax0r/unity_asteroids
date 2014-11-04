using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {

	Color green = new Color(0,255,0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		var cameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, camera.nearClipPlane));
		if (Physics.Raycast(cameraCenter, this.transform.forward, out hit, 1000))
		{
			var obj = hit.transform.gameObject;
	        
			if(obj.name == "Asteroid"){
				if(obj.renderer.material.color != green){
      			obj.renderer.material.color = new Color(0,255,0);
		    	obj.audio.Play ();
				}
			}
			hit.transform.Rotate(1.0f, 1.0f, 1.0f);
		}
	}
}
