﻿using UnityEngine;
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
	public GameObject frontLeftPad;
	public GameObject frontRightPad;
	public GameObject backLeftPad;
	public GameObject backRightPad;
	public Color hitColor;
	public Color hitEmission;
	public Color leftWallColor;
	public Color leftWallEmission;
	public Color rightWallColor;
	public Color rightWallEmission;
	public Color frontLeftColor;
	public Color frontLeftEmission;
	public Color frontRightColor;
	public Color frontRightEmission;
	public Color backLeftColor;
	public Color backLeftEmission;
	public Color backRightColor;
	public Color backRightEmission;
	public GameObject metronome;
	public GameObject blueMarkerPrefab;
	public GameObject yellowMarkerPrefab;
	public GameObject redMarkerPrefab;
	public GameObject greenMarkerPrefab;

	private HandModel[] hands;
	private List<float> fingerXPositions = new List<float>();
	private List<float> fingerYPositions = new List<float>();
	private List<float> fingerZPositions = new List<float>();
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

			//get the x, y and z position of each finger and add it to the list
			for (int i = 0; i < HandModel.NUM_FINGERS;i++) {

				FingerModel finger = hand.fingers[i];

				//add this finger x pos to the list in position i
				float fingerXPos = finger.GetTipPosition().x;
				fingerXPositions.Insert(i, fingerXPos);
				//Debug.Log("xpos finger " + i + ": " + fingerXPositions[i]);

				//add this finger y pos to the list in position i
				float fingerYPos = finger.GetTipPosition().y;
				fingerYPositions.Insert(i, fingerYPos);

				//add this finger y pos to the list in position i
				float fingerZPos = finger.GetTipPosition().z;
				fingerZPositions.Insert(i, fingerZPos);
			}
		}

		/*
		//find the lowest value of finger x positions
		float lowestXPos = Mathf.Min(fingerXPositions[0],fingerXPositions[1],fingerXPositions[2],fingerXPositions[3],fingerXPositions[4]);
		//Debug.Log("lowestXPos: " + lowestXPos);

		//if a finger entered the left wall
		if(lowestXPos <= -4.0f && handInLeftWall == false) {
			handInLeftWall = true;
			StartCoroutine(FadeColor(leftWall,leftWallColor, leftWallEmission));
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
			StartCoroutine(FadeColor(rightWall,rightWallColor, rightWallEmission));
			Instantiate(redMarkerPrefab, new Vector3(metronome.transform.position.x, 5.18f, -0.02f), Quaternion.identity);
		}

		if (highestXPos < 4.0f) {
			handInRightWall = false;
		}*/

		//find the lowest value of finger y positions
		float lowestYPos = Mathf.Min(fingerYPositions[0],fingerYPositions[1],fingerYPositions[2],fingerYPositions[3],fingerYPositions[4]);
		//Debug.Log("lowestYPos: " + lowestYPos);

		//find average x position of fingers
		float averageXPos = (fingerXPositions[0] + fingerXPositions[1] + fingerXPositions[2] + fingerXPositions[3] + fingerXPositions[4]) / 5;
		//Debug.Log("averageXPos: " + averageXPos);
		
		//find average z position of fingers
		float averageZPos = (fingerZPositions[0] + fingerZPositions[1] + fingerZPositions[2] + fingerZPositions[3] + fingerZPositions[4]) / 5;
		Debug.Log("averageZPos: " + averageZPos);

		//if a finger entered the floor
		if(lowestYPos <= -1.2f && handInFloor == false) {
			handInFloor = true;

			//front left pad
			if (averageZPos < -3 && averageXPos < 0) {
				StartCoroutine(FadeColor(frontLeftPad,frontLeftColor, frontLeftEmission));
				Instantiate(redMarkerPrefab, new Vector3(metronome.transform.position.x, 4.43f, -0.02f), Quaternion.identity);
			}
			//front right pad
			else if (averageZPos < -3 && averageXPos >= 0) {
				StartCoroutine(FadeColor(frontRightPad,frontRightColor, frontRightEmission));
				Instantiate(blueMarkerPrefab, new Vector3(metronome.transform.position.x, 4.68f, -0.02f), Quaternion.identity);
			}
			//back left pad
			else if (averageZPos >= -3 && averageXPos < 0) {
				StartCoroutine(FadeColor(backLeftPad,backLeftColor, backLeftEmission));
				Instantiate(yellowMarkerPrefab, new Vector3(metronome.transform.position.x, 4.93f, -0.02f), Quaternion.identity);
			}
			//back right pad
			if (averageZPos >= -3 && averageXPos >= 0) {
				StartCoroutine(FadeColor(backRightPad,backRightColor, backRightEmission));
				Instantiate(greenMarkerPrefab, new Vector3(metronome.transform.position.x, 5.18f, -0.02f), Quaternion.identity);
			}
		}
		
		if (lowestYPos > -1.2f) {
			handInFloor = false;
		}

	}


	public IEnumerator FadeColor(GameObject pad, Color fadeToColor, Color fadeToEmissionColor) {
		Debug.Log("Starting color lerp");
		float ElapsedTime = 0.0f;
		float TotalTime = 0.2f;
		while (ElapsedTime < TotalTime) {
			ElapsedTime += Time.deltaTime;
			pad.GetComponent<Renderer>().material.color = Color.Lerp(hitColor, fadeToColor, (ElapsedTime / TotalTime));
			pad.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(hitEmission, fadeToEmissionColor, (ElapsedTime / TotalTime)));
			yield return null;
		}
		Debug.Log("Ending color lerp");
	}
}
