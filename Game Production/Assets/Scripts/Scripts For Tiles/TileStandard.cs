using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileStandard : MonoBehaviour {
	
	public Material[] controlRingColors;
	public Material defualtMat;
	public UnitBase unitOnTile;
	public Player controller = null;
	public bool isStructure = false;
	public bool canMoveTo = false;
	public bool canAttackUnitOnThis = false;

	public int moveCost;
	public int originalMoveCost = 1;

	//Resource Implementation
	public int ResourceValue = 0;
	public string ResourceType = "";
	public string TerrainName = "";
	public bool hasBeenHarvested = false;
	public bool isControlled = false;

	//UI Stat stuff
	private bool entered = false;
	private float STAT_BOX_X_POS = Screen.width*0.2f;
	private float STAT_BOX_Y_POS = Screen.height*.03f;
	private float STAT_BOX_WIDTH = Screen.width * 0.3f;
	private float STAT_BOX_HEIGHT = Screen.height * 0.1f;

	public virtual void init() {

	}

	public void Start () {
		init ();

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject i in players) {
			if(i.GetComponent<Player>() != null && getControlRingMatName() == i.GetComponent<Player>().getPlayerColor()) {
				this.controller = i.GetComponent<Player>();
			}
		}
		
		moveCost = originalMoveCost;
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

	void OnGUI() {
		STAT_BOX_X_POS = Screen.width * 0.2f;
		STAT_BOX_Y_POS = Screen.height * 0.05f;
		STAT_BOX_WIDTH = Screen.width * 0.225f;
		STAT_BOX_HEIGHT = Screen.height * 0.1f;
		
		GUI.skin.box.alignment = TextAnchor.UpperCenter;
		GUI.color = new Vector4(0.23f, 0.75f, 0.54f, 1);

		if (entered) {
			GUI.color = Color.cyan;
			GUI.Box (new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS*0f, STAT_BOX_WIDTH, STAT_BOX_HEIGHT),
			         this.TerrainName);
			GUI.Box (new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS*0.5f, STAT_BOX_WIDTH, STAT_BOX_HEIGHT),
			         "Resource: " + this.ResourceType +  " " + this.ResourceValue);

			if(this.controller != null) {
				GUI.color = this.controller.getColor();
				GUI.Box (new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS, STAT_BOX_WIDTH, STAT_BOX_HEIGHT),
				         "Owner: " + this.controller.getPlayerID());
			}

		}
	}

	//Called when tile is pressed
	public void OnMouseUpAsButton() {
		Debug.Log ("clicked");
		if (!this.enabled)
		{
			return;
		}

		if (this.unitOnTile == null) {
			this.canAttackUnitOnThis = false;
		}

		if (this.unitOnTile == null && getTerrainMatName ().Equals ("defaultMat") && !isStructure) {
			NewGameController.deselectAllUnits();
		}
		else if (this.unitOnTile != null && this.unitOnTile.controller != NewGameController.currentPlayer && getTerrainMatName ().Equals ("defaultMat") && !isStructure) {
			NewGameController.deselectAllUnits();
		}
		else {
			if(NewGameController.selectedUnit != null) {
				if (this.unitOnTile != null && NewGameController.currentPlayer == this.unitOnTile.controller) {
					this.unitOnTile.selected ();
				} 
				else if(this.unitOnTile == null && NewGameController.selectedUnit != null && canMoveTo) {
					NewGameController.selectedUnit.moveUnit(this);
				}
				else if(this.unitOnTile != null && NewGameController.selectedUnit != null && canAttackUnitOnThis) {
					NewGameController.selectedUnit.attackUnit(this.unitOnTile);
				}
			}
			else if(NewGameController.selectedUnit == null) {
				if (this.unitOnTile != null && NewGameController.currentPlayer == this.unitOnTile.controller) {
					this.unitOnTile.selected ();
				} 
				else if(this.unitOnTile == null && this.isStructure) {
					NewGameController.deselectAllUnits();
					buildingSelected();
				}
			}
		}
	}

	

	void Update () {
		if (unitOnTile != null && (NewGameController.currentPlayer != unitOnTile.controller)) {
			moveCost = 10000;
		}
		else {
			moveCost = originalMoveCost;
		}
	}

	protected virtual void tempModsUpdate()
	{

	}

	public virtual void buildingSelected()
	{
	}

	public virtual void deselectAction ()
	{
	}

	public string getControlRingMatName() {
		string ringName = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material.name;
		ringName = ringName.Substring (0, ringName.IndexOf (" ("));
		return ringName;
	}

	public string getTerrainMatName() {
		string terrainName = transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ().material.name;
		terrainName = terrainName.Substring (0, terrainName.IndexOf (" ("));
		return terrainName;
	}
	
	public void deselect() {
		MeshRenderer planeRenderer = transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = defualtMat;
		canMoveTo = false;
		canAttackUnitOnThis = false;
		deselectAction ();
	}
	
	public void setControl(Player player) {
		MeshRenderer planeRenderer = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = player.playerColor;
		controller = player;
	}

	public virtual void resolveTurn() {
		//GameObject[] Territory = GameObject.FindGameObjectsWithTag ("Tile");
		//foreach (GameObject i in Territory) {

		//Debug.Log (NewGameController.currentPlayer.getPlayerColor ());
		//Debug.Log (this.controller.getPlayerColor ());
			if (this.controller != null && NewGameController.currentPlayer.getPlayerColor() == this.controller.getPlayerColor()) {
				if(this.ResourceType.Equals("Food"))
				{
					this.controller.FoodPool += this.ResourceValue;
				}
				else if(this.ResourceType.Equals("Lumber"))
				{
					this.controller.LumberPool += this.ResourceValue;
				}
				else if(this.ResourceType.Equals("Ore"))
				{
					this.controller.OrePool += this.ResourceValue;
				}
			}
		//}
	}
	
}