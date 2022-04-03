using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardAnimation : MonoBehaviour {
	public static CardAnimation Instance { get; private set; } = null;

	readonly float timeAppear = 0.4f;
	readonly float timeDisappear = 0.3f;
	readonly float timeMove = 0.2f;

	void Awake() {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		//DOTween.Init();
	}

	public void AppearCard(Card card) {
		StartCoroutine(ApearCoroutine(card));
	}
	public void DisappearCard(Card card) {
		StartCoroutine(DisapearCoroutine(card));
	}
	public void MoveCard(Card from, Card to) {
		StartCoroutine(MoveCoroutine(from, to));
	}
	public void ColorizeCardValue(Card card, Color color) {
		StartCoroutine(ColorizeCoroutine(card, color));
	}

	IEnumerator ApearCoroutine(Card card) {
		card.transform.localScale = Vector2.zero;
		Tween myTween = card.transform
			.DOScale(1, timeAppear).SetEase(Ease.OutBack);
		yield return myTween.WaitForCompletion();
	}

	IEnumerator DisapearCoroutine(Card card) {
		Tween myTween = card.transform
			.DOScale(0, timeDisappear).SetEase(Ease.Flash);
		yield return myTween.WaitForCompletion();
		Destroy(card.gameObject);
	}

	IEnumerator MoveCoroutine(Card from, Card to) {
		GameManager.Instance.canMove = false;

		Vector3 toPosition = new Vector3(to.transform.position.x, to.transform.position.y, -1);
		Tween myTween = from.transform
			.DOMove(toPosition, timeMove).SetEase(Ease.OutSine);
		yield return myTween.WaitForCompletion();

		GameManager.Instance.canMove = true;
		GameManager.Instance.CheckGameOver(from);
	}

	IEnumerator ColorizeCoroutine(Card card, Color color) {
		Tween myTween = card.textValue.
			DOColor(color, 0.15f).SetEase(Ease.OutQuint);
		yield return myTween.WaitForCompletion();
		myTween = card.textValue.
			DOColor(Color.white, 0.15f).SetEase(Ease.InQuint);
		yield return myTween.WaitForCompletion();
	}

}
