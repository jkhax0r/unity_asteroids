using UnityEngine;
using System.Collections;

public class MenuLook : MonoBehaviour {
	
	Color green = new Color(0,255,0);
	float sw = Screen.width;
	float sh = Screen.height;

	int quitCounter;
	int startCounter; 

	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		var cameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, camera.nearClipPlane));
		if (Physics.Raycast(cameraCenter, this.transform.forward, out hit, 1000))
		{
			var obj = hit.transform.gameObject;
			
			if(obj.name.Contains("SB_")){
				if(obj.renderer.material.color != green){
					obj.renderer.material.color = new Color(0,255,0);
					startCounter++;
					//if(startCounter > 10){
					Application.LoadLevel("MainScreenScene");
					//}
					
				}
			}else if(obj.name.Contains("QB_")){
				if(obj.renderer.material.color != green){
					obj.renderer.material.color = new Color(0,255,0);
					startCounter++;
					//if(quitCounter > 10){
					Application.Quit();
					//}
				}
			}else{
				quitCounter = 0;
				startCounter = 0;
			}

			
		}
	}
}
