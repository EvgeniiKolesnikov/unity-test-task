using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlusOneUIText : MonoBehaviour {
	float timeAnimation = 0.7f;
	void Start() {
		MoveElement();
	}

	void MoveElement() {
		StartCoroutine(MoveCoroutine());
	}

	IEnumerator MoveCoroutine() {
		Vector3 targetPosition = transform.localPosition + Vector3.up * 40;
		TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();
		text.color = new Color32(255, 255, 255, 0);

		transform.DOLocalMove(targetPosition, timeAnimation).SetEase(Ease.OutSine);

		Tween colorTween = text.
			DOColor(new Color32(255, 255, 255, 255), timeAnimation / 2).SetEase(Ease.Linear);
		yield return colorTween.WaitForCompletion();
		colorTween = text.
			DOColor(new Color32(255, 255, 255, 0), timeAnimation / 2).SetEase(Ease.OutSine);
		yield return colorTween.WaitForCompletion();

		Destroy(gameObject);
	}
}
