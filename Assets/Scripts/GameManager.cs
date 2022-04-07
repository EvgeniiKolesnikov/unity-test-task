using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance { get; private set; } = null;

	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI levelText;
	[SerializeField] TextMeshProUGUI plusOneUIText;

	[Space(10)]
	public TextMeshProUGUI loadingText;
	[SerializeField] TextMeshProUGUI gameOverText;
	[SerializeField] TextMeshProUGUI gameOverText2;

	public int Score { get; private set; } = 0;
	public int Level { get; private set; } = 1;
	public float ChanceRedCard { get; private set; } = 0.1f;

	readonly float maxChanceRedCard = 0.5f;
	public bool canMove = false;

	void Awake() {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		//DontDestroyOnLoad(gameObject);
	}

	void Start() {
		SetChanceRedCard();
		Board.Instance.SetupBoard();
	}

	public void AddScore() {
		Score++;
		SetLevel();
		SetChanceRedCard();
		UpdateUI();
		AddUITextInPosition(plusOneUIText, scoreText);
	}

	void SetLevel() {
		int prevLevel = Level;
		Level = (Mathf.FloorToInt(Score / 10)) + 1;
		if (prevLevel != Level) {
			AddUITextInPosition(plusOneUIText, levelText);
		}
	}

	void UpdateUI() {
		scoreText.text = Score.ToString();
		levelText.text = Level.ToString();
	}

	void AddUITextInPosition(TextMeshProUGUI uiText, TextMeshProUGUI uiPlace) {
		var p = Instantiate(
			uiText,
			uiPlace.transform.position,
			Quaternion.identity,
			uiPlace.gameObject.transform.parent
		);
		p.transform.localPosition += Vector3.down * 36;
	}

	void SetChanceRedCard() {
		float newChance = (float)(Level * 0.1);
		if (newChance < maxChanceRedCard) {
			ChanceRedCard = newChance;
		} else {
			ChanceRedCard = maxChanceRedCard;
		}
	}

	public void SwitchText(TextMeshProUGUI uiText) {
		uiText.gameObject.SetActive(!uiText.gameObject.activeSelf);
	}

	public void CheckGameOver(Card heroCard) {
		if (heroCard.Value <= 0) {
			canMove = false;
			SwitchText(gameOverText);
			SwitchText(gameOverText2);
			Invoke("GameOver", 2);
		}
	}

	void GameOver() {
		ScoreManager.Instance.SaveSessionData(Score, Level);
		SceneController.LoadGameOverScene();
	}

	void OnApplicationQuit() {
		ScoreManager.Instance.SaveSessionData(Score, Level);
	}
}
