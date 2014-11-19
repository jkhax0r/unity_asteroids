using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {

	public GameObject asteroid_prefab;
	public GameObject bullet_prefab;

	public int InterWaveTime = 5;
	public int WaveOneTime = 45;
	public int WaveTwoTime = 60;
	public float BulletRate = 4.0f;
	public float BulletSpeed = 25.0f;

	Color green = new Color(0,255,0);
	float sw = Screen.width;
	float sh = Screen.height;

	private float bullet_rate;
	private float bullet_speed;
	private float lastTime = 0.0f;

	private bool wave_end = true;
	private int current_wave = 0;
	private float wave_start;
	private float wave_time;
	private float wave_total_time;
	private GameObject wave_txt;
	private GameObject wa_txt;
	private GameObject wb_txt;
	public static int GameScore = 0;
	private Color colorHold;



	// Use this for initialization
	void Start () {
		wave_txt = GameObject.Find("GUI_WAVE");
		wa_txt = GameObject.Find("GUI_WAVE_DETAIL_A");
		wb_txt = GameObject.Find("GUI_WAVE_DETAIL_B");

		bullet_rate = BulletRate;
		bullet_speed = BulletSpeed;

		current_wave = 0;
		EndWave ();	// this will start the wave transition
	}

	void SpawnAsteroidRand (float xmin, float xmax, float ymin, float ymax, float zmin, float zmax,
	                   float vxmin, float vxmax, float vymin, float vymax, float vzmin, float vzmax)
	{
		float randX = Random.Range (xmin, xmax);
		float randY = Random.Range (ymin, ymax);
		float randZ = Random.Range (zmin, zmax);
		GameObject o = (GameObject)Instantiate (asteroid_prefab, new Vector3(randX, randY, randZ) * 5.0f, Quaternion.identity );

		randX = Random.Range (vxmin, vxmax);
		randY = Random.Range (vymin, vymax);
		randZ = Random.Range (vzmin, vzmax);
		Vector3 randDirection = new Vector3 (randX, randY, randZ);
		o.rigidbody.velocity = randDirection;

		o.rigidbody.freezeRotation = false;
		o.rigidbody.AddTorque (Random.Range (-1000.0f, 1000.0f), Random.Range (-1000.0f, 1000.0f), Random.Range (-1000.0f, 1000.0f));

	}

	Vector3 RandVector()
	{
		return new Vector3(Random.Range (-1.0f, 1.0f),Random.Range (-1.0f, 1.0f),Random.Range (-1.0f, 1.0f));
	}

	void SpawnAsteroidDirected (float xmin, float xmax, float ymin, float ymax, float zmin, float zmax, float speedmin, float speedmax)	                        
	{
		float randX = Random.Range (xmin, xmax);
		float randY = Random.Range (ymin, ymax);
		float randZ = Random.Range (zmin, zmax);
		GameObject o = (GameObject)Instantiate (asteroid_prefab, new Vector3(randX, randY, randZ) * 5.0f, Quaternion.identity );

		Vector3 dir = (camera.transform.position + RandVector() * 10.0f) - o.transform.position;
		dir.Normalize ();

		o.rigidbody.velocity = dir * Random.Range (speedmin, speedmax);
		o.rigidbody.freezeRotation = false;
		o.rigidbody.AddTorque (Random.Range (-1000.0f, 1000.0f), Random.Range (-1000.0f, 1000.0f), Random.Range (-1000.0f, 1000.0f));
		//o.rigidbody.rotation = new Quaternion (Random.Range (-500.0f, 500.0f), Random.Range (-50.0f, 50.0f), Random.Range (-50.0f, 50.0f), Random.Range (-50.0f, 50.0f));
	}

	void StartWave(int wave)
	{
		GameObject b;

		current_wave = wave;
		wave_end = false;
		wave_start = Time.fixedTime;

		wave_txt.guiText.text = "Wave " + wave;
		wa_txt.guiText.text = "";
		wb_txt.guiText.text = "";
		wave_txt.guiText.enabled = true;
		wa_txt.guiText.enabled = true;
		wb_txt.guiText.enabled = true;

		switch (current_wave) 
		{
		case 1:
			wave_total_time = WaveOneTime;
			wa_txt.guiText.text = "Warmup Round";
			wb_txt.guiText.text = "Destroy as many as you can";
			b = GameObject.Find ("Borders");
			b.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			for (int i = 0; i < 8; ++i) {
				SpawnAsteroidRand (-1.0f, 1.0f, -.4f, .4f, -1.0f, 1.0f, -5.0f, 5.0f, -5.0f, 5.0f, -5.0f, 5.0f);
			}
			break;

		case 2:
			wave_total_time = WaveTwoTime;
			wa_txt.guiText.text = "Asteroid Field";
			wb_txt.guiText.text = "From around the sun!!";

			b = GameObject.Find ("Borders");
			b.transform.localScale = new Vector3(60.0f, 60.0f, 60.0f);			
			for (int i = 0; i < 47; ++i) {
				SpawnAsteroidDirected (-25.0f, -5.0f, -10f, 10f, -110.0f, -50.0f, 10.0f, 20.0f);
			}
			break;

		case 3:
			wave_total_time = WaveTwoTime;
			wa_txt.guiText.text = "Asteroid Field";
			wb_txt.guiText.text = "Weapon Upgrade!";

			bullet_rate = 40;
			bullet_speed = 100;

			b = GameObject.Find ("Borders");
			b.transform.localScale = new Vector3(60.0f, 60.0f, 60.0f);
			for (int i = 0; i < 47; ++i) {
				SpawnAsteroidDirected (-25.0f, -5.0f, -10f, 10f, -110.0f, -50.0f, 10.0f, 20.0f);
			}
			break;
		case 4:
			wave_total_time = WaveOneTime;
			wa_txt.guiText.text = "Final Wave";
			wb_txt.guiText.text = "Destroy as many as you can";
						
			bullet_rate = 40;
			bullet_speed = 100;

			b = GameObject.Find ("Borders");
			b.transform.localScale = new Vector3(60.0f, 60.0f, 60.0f);
			for (int i = 0; i < 500; ++i) {
				SpawnAsteroidDirected (-25.0f, -5.0f, -10f, 10f, -110.0f, -50.0f, 10.0f, 20.0f);
			}

			break;
		case 5:			
			wa_txt.guiText.text = "TODO";
			wb_txt.guiText.text = "That's all";
			
			bullet_rate = 40;
			bullet_speed = 100;
			
			b = GameObject.Find ("Borders");
			b.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
			for (int i = 0; i < 100; ++i) {
				SpawnAsteroidRand (-1.0f, 1.0f, -.4f, .4f, -1.0f, 1.0f, -5.0f, 5.0f, -5.0f, 5.0f, -5.0f, 5.0f);
			}

			break;			
		default:
			break;
		}
	}

	void UpdateWave()
	{
		wave_time = Time.fixedTime - wave_start;
		if (wave_txt.guiText.enabled && (wave_time > 5))
		{
			wave_txt.guiText.enabled = false;
			wa_txt.guiText.enabled = false;
			wb_txt.guiText.enabled = false;
		}

		switch (current_wave) 
		{		
		case 1:
		case 2:
		case 3:
		case 4:
			UpdateTimedWave ();
			break;
		case 5:
			// NO END
			break;
		
		}
	}

	void UpdateTimedWave()
	{
		int remaining = (int)((float)wave_total_time - wave_time);
		GameObject tr = GameObject.Find ("GUI_TEXT_A");
		tr.guiText.enabled = true;
		tr.guiText.text = "Time Remaining: " + (remaining + 1);

		if (remaining <= 8 && tr.guiText.color != Color.red) {
			colorHold = tr.guiText.color;
			tr.guiText.color = Color.red;
		}
		
		if (remaining + 1 <= 0) {
			tr.guiText.color = colorHold;
			tr.guiText.enabled = false;
			EndWave ();
		}

	}

	void EndWave()
	{
		wave_end = true;
		wave_start = Time.fixedTime;
		wave_time = 0;

		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");
		for (int i = 0; i < asteroids.Length; ++i) {
			Destroy (asteroids[i]);
		}

		wave_txt.guiText.enabled = true;
		wa_txt.guiText.enabled = true;
		wb_txt.guiText.enabled = false;
		wa_txt.guiText.text = "Prepare for next wave...";
		wave_txt.guiText.text = "" + (int)(5.0 - wave_time);
	}

	void UpdateWaveEnd()
	{
		wave_time = Time.fixedTime - wave_start;
		wave_txt.guiText.text = "" + (int)((float)InterWaveTime + 1.0f - wave_time);
		if (wave_time >= (float)InterWaveTime) 
		{
			wave_txt.guiText.enabled = false;
			wa_txt.guiText.enabled = false;
			wb_txt.guiText.enabled = false;

			current_wave++;
			StartWave(current_wave);

		}
	}




	// Update is called once per frame
	void FixedUpdate () {

		// Update score
		GameObject s = GameObject.Find ("GUI_SCORE");
		s.guiText.text = "Score: " + GameScore / 2; // I have no idea why the score goes up by two each time

		if (wave_end) 
		{
			UpdateWaveEnd ();
		} else {
			UpdateWave ();
		}

		GameObject[] asteroids = GameObject.FindGameObjectsWithTag ("Asteroid");

		/*
		GameObject t = GameObject.Find ("GUI_TIME");
		int s = (int)Time.time;
		int m = s / 60;
		s -= m * 60;
		string txt = "Time: " + m + ":";
		if (s < 10)	txt += "0";
		txt += s;
		t.guiText.text = txt;
		*/


		if (Time.fixedTime - lastTime > (1.0f/bullet_rate)) {
			lastTime = Time.fixedTime;

			Vector3 p = camera.transform.position + camera.transform.forward * 0.2f;

			GameObject o = (GameObject)Instantiate (bullet_prefab, p, Quaternion.identity );
			o.rigidbody.velocity = camera.transform.forward * bullet_speed;
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
