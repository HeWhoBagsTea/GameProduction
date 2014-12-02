using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewGameController : MonoBehaviour {
	public List<Player> players;
	public AudioClip clickSound;
	public static bool isGameOver = false;
	private int gameoverCount;

	public static Player currentPlayer;
	public static UnitBase selectedUnit;

	public static float xPos = -100;
	public static float yPos = -100;
	public static float yOffset = 0;
	public static int attackingUnitPow = 0;

	public static int AImovePriority = 0;
	public static Vector4 DamageColor = new Vector4(1.0f,0.0f,0.0f,1.0f);

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
		Debug.Log (AImovePriority);

		//isGameOver = true;
		isGameOver = checkIfGameOver ();
		if (isGameOver) {
			Debug.Log("GameOver");
		}

		if (Input.GetMouseButtonDown (0)) {
			audio.PlayOneShot(clickSound);
		}

		if(Input.GetMouseButtonDown(1)) {
			deselectAllUnits();
		}

		if (Input.GetMouseButtonDown (2) && NewGameController.selectedUnit != null) {
			NewGameController.selectedUnit.captureTile(NewGameController.selectedUnit.currentSpace);
		}


		checkTurn ();

	}

	void OnGUI()
	{
		if (isGameOver) 
		{
			GUI.Button(new Rect(Screen.width / 3, Screen.height / 4, Screen.width / 4, Screen.height / 5), "Game Over!\n" + currentPlayer.getPlayerColor() + " Wins!");
		}

		GUI.color = DamageColor;
		GUI.skin.label.fontSize = 28;
		if (GUI.color != Color.red) {
				GUI.Label (new Rect (xPos, yPos - yOffset, 100, 35), "-1");
			} else {
				GUI.Label (new Rect (xPos, yPos - yOffset, 100, 35), "-" + attackingUnitPow);
			}
	}

	private void checkTurn() {
		GameObject[] playersObjs = GameObject.FindGameObjectsWithTag ("Player"); 
		foreach (GameObject i in playersObjs) {
			if(i.GetComponent<Player>() != null && i.GetComponent<Player>() != currentPlayer) {
				i.GetComponent<Player>().isTurn = false;
			}
		}

		currentPlayer.isTurn = true;
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
