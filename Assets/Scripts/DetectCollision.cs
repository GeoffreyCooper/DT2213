using UnityEngine;
using System.Collections;

public class DetectCollision : MonoBehaviour {

	public Color hitColor;
	public Color defaultColor;

	private Color currentColor;
	private Color nextColor;

	private bool gettingFaded = false;
	private bool handIsInWall;


	void Start() {
	
	}

	void FixedUpdate() {
		gettingFaded = false;
	}

	/*
	void Update() {

		if (hitHappened == true) {

			Debug.Log("triggered");

			gameObject.GetComponent<Renderer> ().material.color = Color.Lerp(hitColor, defaultColor, 1.0f);
			hitHappened = false;
		}


	}*/

	/*
	void OnCollisionEnter() {
		gameObject.GetComponent<Renderer> ().material.color = hitColor;
	}

	void OnCollisionExit() {
		gameObject.GetComponent<Renderer> ().material.color = defaultColor;
	}*/

	void OnTriggerEnter() {
		Debug.Log("entered trigger");
		//gameObject.GetComponent<Renderer> ().material.color = hitColor;
		//hitHappened = true;

		//if it's not already fading, start a fade
		if (gettingFaded == false) {
			gettingFaded = true;
			StartCoroutine(FadeColor());
		}

	}
	
	void OnTriggerExit() {
		//gameObject.GetComponent<Renderer> ().material.color = defaultColor;
		//gettingFaded = false;
	}

	void OnTriggerStay() {
		//gameObject.GetComponent<Renderer> ().material.color = hitColor;
		//the hand is in the trigger so it should already be fading
		gettingFaded = true;
	}


	public IEnumerator FadeColor() {
		Debug.Log("Starting color lerp");
		float ElapsedTime = 0.0f;
		float TotalTime = 0.4f;
		while (ElapsedTime < TotalTime) {
			ElapsedTime += Time.deltaTime;
			gameObject.GetComponent<Renderer>().material.color = Color.Lerp(hitColor, defaultColor, (ElapsedTime / TotalTime));
			yield return null;
		}
		Debug.Log("Ending color lerp");
		//done fading
		gettingFaded = false;
	}


}
