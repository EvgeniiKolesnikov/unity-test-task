using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour {
	[SerializeField] TextMeshProUGUI highScoreText;
	[SerializeField] TextMeshProUGUI highLevelText;

	void Start() {
		SetScoreUI();
		SetLevelUI();
	}

	public void SetScoreUI() {
		highScoreText.text = Global.highScore.ToString();
	}

	public void SetLevelUI() {
		highLevelText.text = Global.highLevel.ToString();
	}
}
