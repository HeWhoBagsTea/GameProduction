using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewGameController : MonoBehaviour {
	public List<Player> players;
	public bool isGameOver = false;
	private int gameoverCount;

	public static Player currentPlayer;
	public static UnitBase selectedUnit;

	// Use this for initialization
	void Start () {
		GameObject[] playersObjs = GameObject.FindGameObjectsWithTag ("Player"); 
		foreach (GameObject i in playersObjs) {
			if(i.GetComponent<Player>() != null) {
				this.players.Add(i.GetComponent<Player>());
			}
		}

		foreach (Player i in this.players) {
			if(i.playerID == 1) {
				currentPlayer = i;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (selectedUnit);

		isGameOver = checkIfGameOver ();
		if (isGameOver) {
			Debug.Log("GameOver");
		}
	}
	
	public static void clearHighlights() {
		foreach(TileStandard i in FindObjectsOfType(typeof(TileStandard))) {
			i.deselect();
		}
	}

	public static void deselectAllUnits() {
		foreach (UnitBase i in FindObjectsOfType(typeof(UnitBase))) {
			i.deselect();
			i.isSelected = false;
		}

		selectedUnit = null;
	}

	private bool checkIfGameOver() {
		gameoverCount = 0;
		foreach (Player i in players) {
			if(i.hasLost) {
				gameoverCount++;
			}
		}

		return gameoverCount == players.Count - 1;
	}
}
