using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	public int score;
	public int level;
	//public int highScore;
	//public int highLevel;

	public PlayerData () {
		score = Global.sessionScore;
		level = Global.sessionLevel;
	}
}
