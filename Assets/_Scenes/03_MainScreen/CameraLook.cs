using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {

	public GameObject asteroid_prefab;
	public GameObject bullet_prefab;
	Color green = new Color(0,255,0);
	float sw = Screen.width;
	float sh = Screen.height;

	private float lastTime = 0.0f;


	// Use this for initialization
	void Start () {
		int i;
		for (i = 0; i < 6; ++i) {
			float randX = Random.Range (-1.0f, 1.0f);
			float randY = Random.Range (-0.4f, 0.4f);
			float randZ = Random.Range (-1.0f, 1.0f);
			GameObject o = (GameObject)Instantiate (asteroid_prefab, new Vector3(randX, randY, randZ) * 5.0f, Quaternion.identity );

			randX = Random.Range (-1.0f, 1.0f);
			randY = Random.Range (-1.0f, 1.0f);
			randZ = Random.Range (-1.0f, 1.0f);
			Vector3 randDirection = new Vector3 (randX, randY, randZ);
			o.rigidbody.velocity = randDirection * 2.00f;
		}
	}



	// Update is called once per frame
	void FixedUpdate () {


		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
		GameObject score = GameObject.Find ("GUI_REMAINING");
		score.guiText.text = "Asteroids Remaining: " + asteroids.Length;

		GameObject t = GameObject.Find ("GUI_TIME");
		int s = (int)Time.time;
		int m = s / 60;
		s -= m * 60;
		string txt = "Time: " + m + ":";
		if (s < 10)	txt += "0";
		txt += s;
		t.guiText.text = txt;



		if (Time.fixedTime - lastTime > 0.25) {
			lastTime = Time.fixedTime;

			Vector3 p = camera.transform.position + camera.transform.forward * 0.2f;

			GameObject o = (GameObject)Instantiate (bullet_prefab, p, Quaternion.identity );
			o.rigidbody.velocity = camera.transform.forward * 25;
		}


		/*RaycastHit hit;
		var cameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, camera.nearClipPlane));
		if (Physics.Raycast(cameraCenter, this.transform.forward, out hit, 1000))
		{
			var obj = hit.transform.gameObject;
	        
			if(obj.name.Contains("Asteroid")){
				Debug.Log (obj.name);
				if(obj.renderer.material.color != green){      				



					//obj.renderer.material.color = new Color(255,0,0);
		    		obj.audio.Play ();
					obj.rigidbody.velocity = Vector3.zero;



					ast = (GameObject)Instantiate(obj);
					ast.rigidbody.velocity = Vector3.zero;
					ast = (GameObject)Instantiate(obj);
					ast.rigidbody.velocity = Vector3.zero;

					Destroy(obj);
			     
				}
			}
	
		}*/


	}
}
