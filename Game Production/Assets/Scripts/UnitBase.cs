using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitBase : MonoBehaviour {
	public CodeTileStandard currentSpace;
	public Material[] unitHighlights;
	public Material[] highlights;
	
	private bool showOptions = false;
	
	public int controller = -1;
	public int movement = 1;
	public int minAttackRange = 0;
	public int maxAttackRange = 1;
	public int attackPow = 1;
	public int maxHP = 1;
	public int currentHP = 1;
	public int foodCost = 0;
	public int lumberCost = 0;
	public string unitType = "";
	
	private bool hasMoved = false;
	private bool hasActioned = false;
	public bool isDone = false;
	
	private float BUTTON_X_POS = Screen.width - (Screen.width / 8);
	private float BUTTON_WIDTH = Screen.width/9;
	private float BUTTON_HEIGHT = Screen.height/20;
	private float BUTTON_SPACING = Screen.height/100 + Screen.height/20;

	private float HP_X_POS = Screen.width * 0.45f;
	private float HP_Y_POS = Screen.height * 0.3f;
	private float HP_WIDTH = Screen.width * 0.1f;
	private float HP_HEIGHT = Screen.height * 0.05f;
	private bool entered = false;
	
	public Vector3 posOffset;
	
	public virtual void init () {
		Debug.Log ("has unit");
		posOffset = new Vector3 (0 , .5f, 0);
	}
	
	void Start() {
		init ();
		
		currentSpace = getClosestTile ();
		currentSpace.unitOnTile = this;
		this.transform.position = currentSpace.transform.position + posOffset;
		
		Material tempMat = transform.GetComponent<MeshRenderer> ().material;
		
		for (int i = 0; i < unitHighlights.Length; i++) {
			if(tempMat.name.Substring(0, tempMat.name.IndexOf(" (")).Equals(unitHighlights[i].name)) {
				controller = i;
			}
		}
	}
	
	void Update() {
		BUTTON_X_POS = Screen.width - (Screen.width / 8);
		BUTTON_WIDTH = Screen.width/9;
		BUTTON_HEIGHT = Screen.height/20;
		BUTTON_SPACING = Screen.height/100 + Screen.height/20;

		HP_X_POS = Screen.width * 0.45f;
		HP_Y_POS = Screen.height * 0.3f;
		HP_WIDTH = Screen.width * 0.1f;
		HP_HEIGHT = Screen.height * 0.05f;

		if (hasMoved && hasActioned) {	
			isDone = true;
		}
		
		if (isDone) {
			renderer.material = unitHighlights[controller + 2];
		}
	}

	void OnMouseEnter()
	{
		entered = true;
	}
	void OnMouseExit()
	{
		entered = false;
	}

	void OnGUI() {
		if (entered) {
			if(controller == 1)
			{
				GUI.color = Color.red;
			}
			else
			{
				GUI.color = Color.cyan;
			}
			GUI.Box (new Rect (HP_X_POS, HP_Y_POS, HP_WIDTH, HP_HEIGHT),
		    "HP:" + currentHP + "/" + maxHP);
		}
		if (!showOptions)return;
		GUI.color = Color.white;
		Rect attackButton = new Rect (BUTTON_X_POS, Screen.height - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
		Rect moveButton = new Rect (BUTTON_X_POS, attackButton.position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
		
		if (GUI.Button (attackButton, "Attack")) {
			showAttack();
		}
		
		if (GUI.Button (moveButton, "Move")) {
			showMovement();
		}
	}
	
	public void selected() {
		if (!isDone && this.controller == CodeGameController.playersTurn) {
			showOptions = true;
			highlightCurrentSpace(highlights[1]);
		}
	}
	
	public void deselect() {
		showOptions = false;
	}
	
	private void showAttack() {
		if (!hasActioned) {
			clearHighlights();
			showAttackHelper(maxAttackRange, this.currentSpace, highlights[3]);
			showAttackHelper(minAttackRange, this.currentSpace, highlights[0]);
		}
	}

	private void showAttackHelper(int attackRange, CodeTileStandard tile, Material mat) {
		if (attackRange > 0) {
			Collider[] hitColliders = Physics.OverlapSphere (tile.transform.position, 2);
			List<CodeTileStandard> tiles = new List<CodeTileStandard>();
			
			foreach(Collider i in hitColliders) {
				if(i.GetComponent<CodeTileStandard>() != null) {
					tiles.Add (i.GetComponent<CodeTileStandard>());
				}
			}
			
			for(int i = 0; i < tiles.Count; i++) {
				if(attackRange - 1 >= 0) {
					if((tiles[i].unitOnTile != null && tiles[i].unitOnTile.controller != this.controller) || tiles[i].unitOnTile == null) {
						tiles[i].transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = mat;
					}
					showAttackHelper(attackRange - 1, tiles[i], mat);
				}
			}
		}
	}
	
	public void attackUnit(UnitBase attackTarget) {
		attackTarget.currentHP -= this.attackPow;
		hasActioned = true;
	}
	
	private void showMovement() {
		if (!hasMoved) {
			clearHighlights();
			showMovementRangeHelper(movement, currentSpace);
			highlightCurrentSpace(highlights[1]);
		}
	}
	
	private void showMovementRangeHelper(int moveRange, CodeTileStandard tile) {
		if (moveRange > 0) {
			Collider[] hitColliders = Physics.OverlapSphere (tile.transform.position, 2);
			List<CodeTileStandard> tiles = new List<CodeTileStandard>();
			
			foreach(Collider i in hitColliders) {
				if(i.GetComponent<CodeTileStandard>() != null) {
					tiles.Add (i.GetComponent<CodeTileStandard>());
				}
			}
			
			for(int i = 0; i < tiles.Count; i++) {
				if(moveRange - tiles[i].moveCost >= 0) {
					if(tiles[i].unitOnTile == null) {
						tiles[i].transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = highlights[2];
					}
					showMovementRangeHelper(moveRange - tiles[i].moveCost, tiles[i]);
				}
			}
		}
	}
	
	public void moveUnit(CodeTileStandard moveLocation) {
		currentSpace.unitOnTile = null;
		currentSpace = moveLocation;
		transform.position = currentSpace.transform.position + posOffset;
		currentSpace.unitOnTile = this;
		hasMoved = true;
	}
	
	public void resolveTurn() {
		renderer.material = unitHighlights[controller];
		hasMoved = false;
		hasActioned = false;
		isDone = false;
	}
	
	public void highlightCurrentSpace(Material highlight) {
		MeshRenderer currentSpaceTile = currentSpace.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer> ();
		currentSpaceTile.material = highlight;
	}
	
	private CodeTileStandard getClosestTile() {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		GameObject closest = null;
		foreach(GameObject i in tiles) {
			if(!closest) {
				closest = i;
			}
			
			if(Vector3.Distance(this.transform.position, i.transform.position) <= Vector3.Distance(this.transform.position, closest.transform.position)) {
				closest = i;
			}
		}
		
		return closest.GetComponent<CodeTileStandard>();
	}
	
	private void clearHighlights() {
		foreach(CodeTileStandard i in FindObjectsOfType(typeof(CodeTileStandard))) {
			i.deselect();
		}
	}
}
