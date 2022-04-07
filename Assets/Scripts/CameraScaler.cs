using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScaler : MonoBehaviour {
	private Camera componentCamera;

	public Vector2 DefaultResolution = new Vector2(500, 700);
	private float initialSize;
	private float targetAspect;
	private float cameraAspect;

	void Awake() {
		componentCamera = GetComponent<Camera>();
	}

	void Start() {
		SetCameraSize(Global.boardSize, Global.spaceH);
		initialSize = componentCamera.orthographicSize;				// 17.5
		targetAspect = DefaultResolution.x / DefaultResolution.y;   // 0.7142857
		cameraAspect = componentCamera.aspect;	// different		// if aspect 0.5 -> need size 25
	}

	void Update() {
		cameraAspect = componentCamera.aspect;
		if (componentCamera.orthographic) {
			float targetSize = initialSize * (targetAspect / componentCamera.aspect);
			if (cameraAspect < targetAspect) {
				componentCamera.orthographicSize = targetSize;
			} else {
				componentCamera.orthographicSize = initialSize;
			}
		}

	}

	void SetCameraSize(int boardSize, float cardHeight) {
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "Game") {
			componentCamera.orthographicSize = (boardSize + 2) * cardHeight / 2;
		} else {
			componentCamera.orthographicSize = 17.5f;
		}

	}
}
