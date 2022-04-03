using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
	public static void RestartScene() {
		DOTween.KillAll();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public static void LoadGameScene() {
		DOTween.KillAll();
		SceneManager.LoadScene("Game");
	}

	public static void LoadGameOverScene() {
		DOTween.KillAll();
		SceneManager.LoadScene("GameOver");
	}

	public static void LoadMenuScene() {
		DOTween.KillAll();
		SceneManager.LoadScene("Menu");
	}

	public static void LoadScene(int boardSize) {
		DOTween.KillAll();
		Global.boardSize = boardSize;
		SceneManager.LoadScene("Game");
	}

	public static void QuitGame() {
		DOTween.KillAll();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
