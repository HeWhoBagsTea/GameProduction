using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewGameController : MonoBehaviour {
	public List<Player> players;
	public bool isGameOver = false;
	private int gameoverCount;

	public static Player currentPlayer;
	public static UnitBase selectedUnit;

	private float BOX_X_POS = Screen.width*0.4f;
	private float BOX_Y_POS = Screen.height*.05f;
	private float BOX_WIDTH = Screen.width * 0.3f;
	private float BOX_HEIGHT = Screen.height * 0.1f;
	
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
		isGameOver = checkIfGameOver ();
		if (isGameOver) {
			Debug.Log("GameOver");
		}
	}

	public void OnGUI() 
	{
		BOX_X_POS = Screen.width * 0.4f;
		BOX_Y_POS = Screen.height * 0.05f;
		BOX_WIDTH = Screen.width * 0.225f;
		BOX_HEIGHT = Screen.height * 0.1f;

		GUI.skin.box.alignment = TextAnchor.UpperCenter;
		GUI.color = new Vector4(0.23f, 0.75f, 0.54f, 1);
		if (selectedUnit != null) {
			GUI.Box (new Rect (BOX_X_POS, BOX_Y_POS*0f, BOX_WIDTH, BOX_HEIGHT), 
			         "Unit Stats:");
			GUI.Box(new Rect (BOX_X_POS, BOX_Y_POS, BOX_WIDTH, BOX_HEIGHT),
			        "HP:" + selectedUnit.HPcurr + "/"+selectedUnit.HPmax +
			        " AttackRange: " + selectedUnit.minAttackRange + "-"+ selectedUnit.maxAttackRange);
			GUI.Box(new Rect (BOX_X_POS, BOX_Y_POS*2f, BOX_WIDTH, BOX_HEIGHT),
			        "Movement: " + selectedUnit.movement +
			        " Attack Power: " + selectedUnit.attackPow);
			GUI.Box(new Rect (BOX_X_POS, BOX_Y_POS*3f, BOX_WIDTH, BOX_HEIGHT),
			        "Unit Type: " + selectedUnit.unitType);
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
