using UnityEngine;
using TMPro;

public class Card : MonoBehaviour {
	public float X { get; private set; }
	public float Y { get; private set; }
	public int Value { get; private set; }
	public CardType Type { get; private set; }

	public TextMeshPro textValue;
	[SerializeField] private SpriteRenderer cardOverlay;
	[SerializeField] private Sprite heroOverlay;
	[SerializeField] private Sprite greenOverlay;
	[SerializeField] private Sprite redOverlay;

	public enum CardType {
		HeroCard,
		RedCard,
		GreenCard
	}

	public void SetValue(float x, float y, CardType type) {
		X = x;
		Y = y;
		Value = RandomValue();
		Type = type;
		cardOverlay.sprite = GetOverlay(type);
		if (type == CardType.HeroCard) {
			transform.position += Vector3.back;
		}
		RefreshText();
	}

	int RandomValue() {
		int randomValue = 1 + Random.Range(0, GameManager.Instance.Level + 1);
		return randomValue;
	}

	void RefreshText() {
		textValue.text = Value.ToString();
	}

	Sprite GetOverlay(CardType type) {
		switch (type) {
			case CardType.HeroCard:
				return heroOverlay;
			case CardType.RedCard:
				return redOverlay;
			case CardType.GreenCard:
				return greenOverlay;
			default:
				return cardOverlay.sprite;
		}
	}

	void OnMouseDown() {
		//print($"click {this.Type} {gameObject.transform.position}");
		if (GameManager.Instance.canMove) {
			FindHeroCard(5, 0);
			FindHeroCard(-5, 0);
			FindHeroCard(0, 7);
			FindHeroCard(0, -7);
		}
	}

	void FindHeroCard(int xDir, int yDir) {
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2(xDir, yDir);
		RaycastHit2D hit;
		//Debug.DrawLine(start, end, Color.yellow);
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		hit = Physics2D.Linecast(start, end);
		gameObject.GetComponent<BoxCollider2D>().enabled = true;
		if (hit.transform == null) {
			return;
		}
		Card hittedCard = hit.collider.gameObject.GetComponent<Card>();
		if (hittedCard) {
			if (hittedCard.Type == CardType.HeroCard) {
				//print("HeroCard founded");
				UpdateHeroCard(hittedCard);
			}
		}
	}

	void UpdateHeroCard(Card heroCard) {
		if (this.Type == CardType.GreenCard) {
			heroCard.Value += this.Value;
			heroCard.RefreshText();
			CardAnimation.Instance.ColorizeCardValue(heroCard, new Color32(31, 150, 27, 255));
		}
		if (this.Type == CardType.RedCard) {
			heroCard.Value -= this.Value;
			heroCard.RefreshText();
			CardAnimation.Instance.ColorizeCardValue(heroCard, new Color32(150, 27, 26, 255));
		}
		CardAnimation.Instance.DisappearCard(this);
		CardAnimation.Instance.MoveCard(heroCard, this);
		Board.Instance.AddNewCardInHeroPlace(heroCard);
		GameManager.Instance.AddScore();
	}

	void Start() {
		CardAnimation.Instance.AppearCard(this);
	}
}
