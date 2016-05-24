using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using System.Linq;

public class DetectCollisionByFingerPosition : MonoBehaviour {
	
	//HandModel hand_model;
	//Hand leap_hand;

	public HandController handController;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject floor;
	public Color hitColor;
	public Color leftWallColor;
	public Color rightWallColor;
	public Color floorColor;
	public GameObject metronome;
	public GameObject blueMarkerPrefab;
	public GameObject yellowMarkerPrefab;
	public GameObject redMarkerPrefab;

	private HandModel[] hands;
	private List<float> fingerXPositions = new List<float>();
	private List<float> fingerYPositions = new List<float>();
	private bool handInLeftWall = false;
	private bool handInRightWall = false;
	private bool handInFloor = false;

	void Start () {

		//HandController handController = GameObject.Find("HandControllerSandBox").transform.getComponent<HandController>();
		//hands = handController.GetAllGraphicsHands();

		/*
		hand_model = GetComponent<HandModel>();
		leap_hand = hand_model.GetLeapHand();
		if (leap_hand == null) Debug.LogError("No leap_hand founded");*/

		/*
		List<Finger> fingers = leap_hand.Fingers;
		foreach(Finger finger in fingers){
		}*/
	}
	

	void Update () {

		hands = handController.GetAllGraphicsHands();

		foreach(HandModel hand in hands) {

			//get the x and y position of each finger and add it to the list
			for (int i = 0; i < HandModel.NUM_FINGERS;i++) {

				FingerModel finger = hand.fingers[i];

				//add this finger x pos to the list in position i
				float fingerXPos = finger.GetTipPosition().x;
				fingerXPositions.Insert(i, fingerXPos);
				//Debug.Log("xpos finger " + i + ": " + fingerXPositions[i]);

				//add this finger y pos to the list in position i
				float fingerYPos = finger.GetTipPosition().y;
				fingerYPositions.Insert(i, fingerYPos);
			}
		}

		//find the lowest value of finger x positions
		float lowestXPos = Mathf.Min(fingerXPositions[0],fingerXPositions[1],fingerXPositions[2],fingerXPositions[3],fingerXPositions[4]);
		//Debug.Log("lowestXPos: " + lowestXPos);

		//if a finger entered the left wall
		if(lowestXPos <= -4.0f && handInLeftWall == false) {
			handInLeftWall = true;
			StartCoroutine(FadeColor(leftWall,leftWallColor));
			Instantiate(yellowMarkerPrefab, new Vector3(metronome.transform.position.x, 4.93f, -0.02f), Quaternion.identity);
		}

		if (lowestXPos > -4.0f) {
			handInLeftWall = false;
		}

		//find the highest value of finger x positions
		float highestXPos = Mathf.Max(fingerXPositions[0],fingerXPositions[1],fingerXPositions[2],fingerXPositions[3],fingerXPositions[4]);
		//Debug.Log("highestXPos: " + highestXPos);

		if (highestXPos >= 4.0f && handInRightWall == false) {
			handInRightWall = true;
			StartCoroutine(FadeColor(rightWall,rightWallColor));
			Instantiate(redMarkerPrefab, new Vector3(metronome.transform.position.x, 5.18f, -0.02f), Quaternion.identity);
		}

		if (highestXPos < 4.0f) {
			handInRightWall = false;
		}

		//find the lowest value of finger y positions
		float lowestYPos = Mathf.Min(fingerYPositions[0],fingerYPositions[1],fingerYPositions[2],fingerYPositions[3],fingerYPositions[4]);
		//Debug.Log("lowestYPos: " + lowestYPos);
		
		//if a finger entered the floor
		if(lowestYPos <= -1.2f && handInFloor == false) {
			handInFloor = true;
			StartCoroutine(FadeColor(floor,floorColor));
			Instantiate(blueMarkerPrefab, new Vector3(metronome.transform.position.x, 4.68f, -0.02f), Quaternion.identity);
		}
		
		if (lowestYPos > -1.2f) {
			handInFloor = false;
		}

	}


	public IEnumerator FadeColor(GameObject wall, Color fadeToColor) {
		Debug.Log("Starting color lerp");
		float ElapsedTime = 0.0f;
		float TotalTime = 0.2f;
		while (ElapsedTime < TotalTime) {
			ElapsedTime += Time.deltaTime;
			wall.GetComponent<Renderer>().material.color = Color.Lerp(hitColor, fadeToColor, (ElapsedTime / TotalTime));
			yield return null;
		}
		Debug.Log("Ending color lerp");
	}
}
