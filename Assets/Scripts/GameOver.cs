using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour {
	[SerializeField] TextMeshProUGUI scoreSessionText;
	[SerializeField] TextMeshProUGUI levelSessionText;

	void Start() {
		SetScoreUI();
		SetLevelUI();
		ScoreManager.Instance.SaveHighData();
	}

	public void SetScoreUI() {
		scoreSessionText.text = Global.sessionScore.ToString();
	}

	public void SetLevelUI() {
		levelSessionText.text = Global.sessionLevel.ToString();
	}
}
