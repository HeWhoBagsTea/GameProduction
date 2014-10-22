using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitBase : MonoBehaviour {

	public Player controller;
	public TileStandard currentSpace;
	public Material[] unitColors;
	public Material[] spaceHighlights;

	private bool isSelected = false;
	private bool hasMoved = false;
	private bool hasActioned = false;
	public bool isDone = false;

	//Units Stats
	public int movement = 2;
	public int minAttackRange = 1;
	public int maxAttackRange = 1;
	public int attackPow = 1;
	public int HPmax = 1;
	public int HPcurr = 1;
	public int foodCost = 0;
	public int lumberCost = 0;
	public string unitType = "";

	public Vector3 posOffset;

	//Button Stuff;
	private float BUTTON_X_POS = Screen.width - (Screen.width / 8);
	private float BUTTON_WIDTH = Screen.width/9;
	private float BUTTON_HEIGHT = Screen.height/20;
	private float BUTTON_SPACING = Screen.height/100 + Screen.height/20;

	//Unit Hp Stuff;
	private float HP_X_POS = Screen.width * 0.45f;
	private float HP_Y_POS = Screen.height * 0.3f;
	private float HP_WIDTH = Screen.width * 0.1f;
	private float HP_HEIGHT = Screen.height * 0.05f;
	private bool entered = false;
	
	//use this to modify unit stats
	public virtual void init() {
		posOffset = new Vector3 (0 , .5f, 0);
	}

	// Use this for initialization
	void Start () {
		init ();

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject i in players) {
			if(i.GetComponent<Player>() != null && getMaterialName() == i.GetComponent<Player>().getPlayerColor()) {
				this.controller = i.GetComponent<Player>();
			}
		}

		this.currentSpace = getClosestTile ();
		this.currentSpace.unitOnTile = this;
		this.transform.position = this.currentSpace.transform.position + this.posOffset;
	}

	//Called when mouse is over unit
	void OnMouseEnter()
	{
		entered = true;
	}

	//Called when mouse leaves unit
	void OnMouseExit()
	{
		entered = false;
	}
	
	//Called when unit is pressed
	void OnMouseUpAsButton() {
		if (NewGameController.currentPlayer == this.controller) {
			this.selected ();
		}
		else if (NewGameController.selectedUnit != null && this.currentSpace.getTerrainMatName().Equals("AttackSpace")) {
			NewGameController.selectedUnit.attackUnit(this);
		}
		else {
			NewGameController.deselectAllUnits();
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.hasMoved && this.hasActioned) {
			this.isDone = true;
			this.renderer.material = this.unitColors[this.controller.playerID + (this.unitColors.Length / 2)];
		}

		if (this.HPcurr <= 0) {
			Destroy(this.gameObject);
		}
	}

	void OnGUI() {
		BUTTON_X_POS = Screen.width - (Screen.width / 8);
		BUTTON_WIDTH = Screen.width/9;
		BUTTON_HEIGHT = Screen.height/20;
		BUTTON_SPACING = Screen.height/100 + Screen.height/20;
		
		HP_X_POS = Screen.width * 0.45f;
		HP_Y_POS = Screen.height * 0.3f;
		HP_WIDTH = Screen.width * 0.1f;
		HP_HEIGHT = Screen.height * 0.05f;

		if (entered) {
			GUI.color = (this.controller == NewGameController.currentPlayer) ? Color.green : Color.red;
			GUI.Box (new Rect (HP_X_POS, HP_Y_POS, HP_WIDTH, HP_HEIGHT),
			         "HP:" + this.HPcurr + "/" + this.HPmax);
		}

		if (isSelected) {
			Rect attackButton = new Rect (BUTTON_X_POS, Screen.height - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
			Rect moveButton = new Rect (BUTTON_X_POS, attackButton.position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);

			GUI.color = (!this.hasActioned) ? Color.white : Color.gray;
			if (GUI.Button (attackButton, "Attack")) {
					showAttack();
			}

			GUI.color = (!this.hasMoved) ? Color.white : Color.gray;
			if (GUI.Button (moveButton, "Move")) {
					showMovement();
			}
		}
	}

	public void attackUnit (UnitBase target) {
		target.HPcurr -= this.attackPow;
		this.hasActioned = true;
		deselect ();
	}

	public void moveUnit(TileStandard moveLocation) {
		this.currentSpace.unitOnTile = null;
		this.currentSpace = moveLocation;
		this.currentSpace.unitOnTile = this;
		this.transform.position = this.currentSpace.transform.position + this.posOffset;
		this.hasMoved = true;
		deselect ();
	}

	public void resolveTurn() {
		this.hasMoved = false;
		this.hasActioned = false;
		this.isDone = false;
		this.renderer.material = this.unitColors[this.controller.playerID];
		deselect ();
	}

	public void selected () {
		NewGameController.deselectAllUnits ();
		NewGameController.selectedUnit = this;
		highlightCurrentSpace (spaceHighlights[1]);
		isSelected = true;
	}

	public void deselect () {
		if (NewGameController.selectedUnit == this) {
			NewGameController.selectedUnit = null;
		}

		NewGameController.clearHighlights ();
		isSelected = false;
	}

	public string getMaterialName() {
		string matName = renderer.material.name;
		matName = matName.Substring (0, matName.IndexOf (" ("));

		return matName;
	}

	private void highlightCurrentSpace(Material highlight) {
		MeshRenderer currentSpaceTile = currentSpace.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer> ();
		currentSpaceTile.material = highlight;
	}

	private void showAttack() {
		if (!hasActioned) {
			NewGameController.clearHighlights();
			showAttackHelper(this.maxAttackRange, this.currentSpace, this.spaceHighlights[3]);
			showAttackHelper(this.minAttackRange - 1, this.currentSpace, this.spaceHighlights[0]);
			highlightCurrentSpace(this.spaceHighlights[1]);
		}
	}
	
	private void showAttackHelper(int attackRange, TileStandard tile, Material mat) {
		if (attackRange > 0) {
			Collider[] hitColliders = Physics.OverlapSphere (tile.transform.position, 2);
			List<TileStandard> tiles = new List<TileStandard>();
			
			foreach(Collider i in hitColliders) {
				if(i.GetComponent<TileStandard>() != null) {
					tiles.Add (i.GetComponent<TileStandard>());
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

	private void showMovement() {
		if (!hasMoved) {
			NewGameController.clearHighlights();
			showMovementRangeHelper(this.movement, this.currentSpace);
			highlightCurrentSpace(this.spaceHighlights[1]);
		}
	}
	
	private void showMovementRangeHelper(int moveRange, TileStandard tile) {
		if (moveRange > 0) {
			Collider[] hitColliders = Physics.OverlapSphere (tile.transform.position, 2);
			List<TileStandard> tiles = new List<TileStandard>();
			
			foreach(Collider i in hitColliders) {
				if(i.GetComponent<TileStandard>() != null) {
					tiles.Add (i.GetComponent<TileStandard>());
				}
			}
			
			for(int i = 0; i < tiles.Count; i++) {
				if(moveRange - tiles[i].moveCost >= 0) {
					if(tiles[i].unitOnTile == null) {
						tiles[i].transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = this.spaceHighlights[2];
					}
					showMovementRangeHelper(moveRange - tiles[i].moveCost, tiles[i]);
				}
			}
		}
	}
	
	private TileStandard getClosestTile() {
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
		
		return closest.GetComponent<TileStandard>();
	}
}
