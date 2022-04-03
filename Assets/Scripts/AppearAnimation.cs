using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AppearAnimation : MonoBehaviour {
	void Start() {
		AppearElement();
	}

	void AppearElement() {
		StartCoroutine(ApearCoroutine());
	}

	IEnumerator ApearCoroutine() {
		transform.localScale = Vector2.zero;
		Tween myTween = transform.DOScale(1, 1.4f).SetEase(Ease.OutBack);
		yield return myTween.WaitForCompletion();
	}
}
