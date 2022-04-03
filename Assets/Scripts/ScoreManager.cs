using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager Instance { get; private set; } = null;

	//public int Score { get; private set; } = 0;
	//public int Level { get; private set; } = 1;
	//public int HighScore { get; private set; } = Global.highScore;
	//public int HighLevel { get; private set; } = Global.highLevel;

	void Awake() {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		LoadHighData();
		//SaveManager.SavePlayer();	
	}

	public void SaveHighData() {
		//print("SaveData");
		if (Global.boardSize == 3) {
			LoadHighData();
			if (Global.sessionScore > Global.highScore) {
				SaveManager.SavePlayer();
			}
		}
	}

	public void LoadHighData() {
		PlayerData playerData = SaveManager.LoadPlayer();
		if (playerData != null) {
			//HighScore = playerData.highScore;
			//HighLevel = playerData.highLevel;
			Global.highScore = playerData.highScore;
			Global.highLevel = playerData.highLevel;
		}
	}

	public void SaveSessionData(int sessionScore, int sessionLevel) {
		Global.sessionScore = sessionScore;
		Global.sessionLevel = sessionLevel;
		SaveHighData();
	}

	//private void OnApplicationQuit() {
	//	SaveHighData();
	//}
}
