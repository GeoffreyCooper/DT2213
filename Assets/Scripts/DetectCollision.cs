using UnityEngine;
using System.Collections;

public class DetectCollision : MonoBehaviour {

	public Color hitColor;
	public Color defaultColor;

	void OnCollisionEnter() {
		gameObject.GetComponent<Renderer> ().material.color = hitColor;
	}

	void OnCollisionExit() {
		gameObject.GetComponent<Renderer> ().material.color = defaultColor;
	}


}
