using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
	public int highScore;
	public int highLevel;
	//public int highScore;
	//public int highLevel;

	public PlayerData () {
		highScore = Global.sessionScore;
		highLevel = Global.sessionLevel;
	}
}
