using UnityEngine;

public class Background : MonoBehaviour {
	private SpriteRenderer spriteRenderer;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		float width = Global.boardSize % 2 == 0 ? 500 : 505;
		float height = Global.boardSize % 2 == 0 ? 700 : 707;
		spriteRenderer.size = new Vector2(width, height);
	}
}
