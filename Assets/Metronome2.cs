using UnityEngine;
using System.Collections;

public class Metronome2 : MonoBehaviour {


	public Transform startMarker;
	public Transform endMarker;
	public float time;

	void Update() {
		if (Input.GetKeyDown(KeyCode.R))
			StartCoroutine(MoveOverTime());
		
	}
	
	IEnumerator MoveOverTime() {
		while(true) {
			float rate = 1.0f/time;
			float index = 0.0f;
			while( index < 1.0f )
			{
				transform.position = Vector3.Lerp( startMarker.position, endMarker.position, index );
				index += rate * Time.deltaTime;
				yield return null;
			}
			transform.position = endMarker.position;
			//start over
			index = 0.0f;
		}
	}
}
