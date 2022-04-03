using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
	public static Board Instance { get; private set; }

	private readonly int boardSize = Global.boardSize;
	private readonly float spaceH = Global.spaceH;
	private readonly float spaceW = Global.spaceW;

	[SerializeField] Card cardPrefab; //Prefab of Card

	// A list of possible locations to place cards
	private List<Vector3> freePositions = new List<Vector3>();

	void Awake() {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}

	void InitialiseList() {
		freePositions.Clear();
		float boardWidth = boardSize * spaceW;
		float boardHeight = boardSize * spaceH;
		float startX = -(boardWidth / 2) + spaceW / 2;
		float startY = -(boardHeight / 2) + spaceH / 2;
		for (int x = 0; x < boardSize; x++) {
			for (int y = 0; y < boardSize; y++) {
				var position = new Vector3(startX + (x * spaceW), startY + (y * spaceH));
				freePositions.Add(position);
			}
		}
	}

	Vector3 RandomPosition() {
		int randomIndex = Random.Range(0, freePositions.Count);
		Vector3 randomPosition = freePositions[randomIndex];
		freePositions.RemoveAt(randomIndex);
		return randomPosition;
	}

	void AddCard(Card.CardType type) {
		if (freePositions.Count == 0) return;

		Card card = Instantiate(cardPrefab, transform, true);
		Vector3 position = RandomPosition();
		card.transform.position = position;
		card.SetValue(position.x, position.y, type);
	}

	public void AddRandomCard() {
		bool isRedCard = Random.Range(0, 1f) <= GameManager.Instance.ChanceRedCard;
		if (isRedCard) {
			AddCard(Card.CardType.RedCard);
		} else {
			AddCard(Card.CardType.GreenCard);
		}
	}

	void AddHeroCard() {
		AddCard(Card.CardType.HeroCard);
	}

	IEnumerator GenerateBoard() {
		yield return new WaitForSeconds(.05f);
		AddHeroCard();
		while (freePositions.Count > 0) {
			yield return new WaitForSeconds(.05f);
			AddRandomCard();
		}
		yield return new WaitForSeconds(.05f);
		GameManager.Instance.SwitchText(GameManager.Instance.loadingText);
		GameManager.Instance.canMove = true;
	}

	public void SetupBoard() {
		InitialiseList();
		StartCoroutine(GenerateBoard());
	}

	public void AddNewCardInHeroPlace(Card heroCard) {
		freePositions.Add(heroCard.transform.position + Vector3.forward);
		AddRandomCard();
	}
}
