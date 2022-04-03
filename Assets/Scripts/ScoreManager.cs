using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager Instance { get; private set; } = null;

	public int Score { get; private set; } = Global.sessionScore;
	public int Level { get; private set; } = Global.sessionLevel;
	public int HighScore { get; private set; } = Global.highScore;
	public int HighLevel { get; private set; } = Global.highLevel;

	void Awake() {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		LoadHighData();
	}
	public void SaveHighData() {
		//print("SaveData");
		if (Global.boardSize == 3) {
			LoadHighData();
			if (Score > Global.highScore) {
				SaveManager.SavePlayer();
			}
		}
	}

	public void LoadHighData() {
		PlayerData playerData = SaveManager.LoadPlayer();
		if (playerData != null) {
			HighScore = playerData.score;
			HighLevel = playerData.level;
			Global.highScore = playerData.score;
			Global.highLevel = playerData.level;
		}
	}

	public void SaveSessionData(int sessionScore, int sessionLevel) {
		Global.sessionScore = sessionScore;
		Global.sessionLevel = sessionLevel;
	}

	private void OnApplicationQuit() {
		SaveHighData();
	}
}
