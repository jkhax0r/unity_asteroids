using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject bullet_prefab;
	public float asteroid_speed = 1.0f;
	bool hitSomething = false;

	// Use this for initialization
	void Start () {
		
	}

	void newAsteroid(GameObject go, float scale, float speed)
	{
		GameObject o = (GameObject)Instantiate(go, 
		                                       go.transform.position + 
		                                       new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), 
		                                       Quaternion.identity);
		o.transform.localScale = new Vector3(scale, scale, scale);
		
		float randX = Random.Range (-1.0f, 1.0f);
		float randY = Random.Range (-1.0f, 1.0f);
		float randZ = Random.Range (-1.0f, 1.0f);
		Vector3 randDirection = new Vector3 (randX, randY, randZ);
		o.rigidbody.velocity = randDirection * speed;
	}
	
	void OnCollisionEnter(Collision co)
	{
		Collider c = co.collider;

		if (gameObject.tag != "Bullet")
			return;

		if (c.collider.tag == "Borders") 
		{
			Destroy (gameObject);
		} else if (c.collider.tag == "Asteroid") {

			if (hitSomething) 
				return;
			hitSomething = true;

			Destroy (gameObject);

			c.audio.Play();
			c.rigidbody.velocity = Vector3.zero;

			float x = c.transform.localScale.x;

			c.transform.localScale = new Vector3(0,0,0);
			Destroy(c.gameObject, 2.1f);


			float speed = asteroid_speed;
			float scale = 0.0f;
			if (x > 1.0f)  {
				speed *= 1.0f;
				scale = 0.8f;
			} else if (x >= 0.8f) {
				speed *= 1.4f;
				scale = 0.6f;
			} else if (x >= 0.6f) {
				speed *= 1.8f;
				scale = 0.4f;
			}

			Debug.Log ("speed = " + speed + " scale = " + scale + "mag = " + c.gameObject.transform.localScale.magnitude);
			
			if (scale >= 0.4f) {
				newAsteroid(c.gameObject, scale, speed);
				newAsteroid(c.gameObject, scale, speed);
			}



		} 

	}

	// Update is called once per frame
	void Update () {
		if (this.transform.position.magnitude > 100) {
			Destroy (gameObject);
		}
	}
}
