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
	}

	public void SetScoreUI() {
		scoreSessionText.text = ScoreManager.Instance.Score.ToString();
	}

	public void SetLevelUI() {
		levelSessionText.text = ScoreManager.Instance.Level.ToString();
	}
}
