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
	private bool isEnabled = true;
	public GUISkin mySkin;

	public int moveCost;
	public int originalMoveCost = 1;
	public int defensiveValue = 0;

	//Resource Implementation
	public int ResourceValue = 0;
	public string ResourceType = "";
	public string TerrainName = "";
	public bool hasBeenHarvested = false;
	public bool isControlled = false;

	//UI Stat stuff
	private bool entered = false;
	private float STAT_BOX_X_POS = 10;
	private float STAT_BOX_Y_POS = 10;
	private float STAT_BOX_WIDTH = Screen.width/9;
	private float STAT_BOX_HEIGHT = Screen.height/30;
	private float STAT_BOX_OFFSET = Screen.height/100 + Screen.height/30;


	private float HP_X_POS; //(Works just fine without this) = Screen.width * 0.45f;
	private float HP_Y_POS; //(Works just fine without this) = 0;
	private float HP_WIDTH = 100;

	//Text Sizing
	private int TextSize = (int)Screen.height/50;

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
		GUI.skin.box.alignment = TextAnchor.UpperCenter;
		GUI.skin.box.fontSize = TextSize;
		GUI.skin.label.fontSize = TextSize;
		GUI.color = new Vector4(0.23f, 0.75f, 0.54f, 1);
		
		if (entered) {

			if(unitOnTile != null) {

				if(this.unitOnTile.controller == NewGameController.currentPlayer){
					GUI.color = new Vector4(0.2f, 1.0f, 0.2f, 1.0f);
				}
				else {
					GUI.color =new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
				}

				HP_X_POS = Camera.main.WorldToScreenPoint (this.unitOnTile.transform.position).x - (HP_WIDTH/2);
				HP_Y_POS = Screen.height - Camera.main.WorldToScreenPoint (this.unitOnTile.transform.position).y - 40;

				GUI.skin.box.fontStyle = FontStyle.Bold;
				GUI.Label (new Rect (HP_X_POS, HP_Y_POS - STAT_BOX_OFFSET, HP_WIDTH, STAT_BOX_HEIGHT),
				           "HP:" + this.unitOnTile.HPcurr + "/" + this.unitOnTile.HPmax, mySkin.GetStyle("Box"));


				STAT_BOX_X_POS = Camera.main.WorldToScreenPoint (this.unitOnTile.transform.position).x - (STAT_BOX_WIDTH/2);
				STAT_BOX_Y_POS = Screen.height - Camera.main.WorldToScreenPoint (this.unitOnTile.transform.position).y + 30;
				
				
				GUI.color = Color.cyan;
				GUI.Label (new Rect (STAT_BOX_X_POS+(STAT_BOX_WIDTH/4), STAT_BOX_Y_POS, STAT_BOX_WIDTH/2, STAT_BOX_HEIGHT*2),
			         this.TerrainName + "\n" + this.ResourceType +  " " + this.ResourceValue, mySkin.GetStyle("Box"));

			}
			else {
				STAT_BOX_X_POS = Camera.main.WorldToScreenPoint (this.transform.position).x - (STAT_BOX_WIDTH/2);
				STAT_BOX_Y_POS = Screen.height - Camera.main.WorldToScreenPoint (this.transform.position).y - 30;
				
				
				GUI.color = Color.cyan;
				GUI.Label (new Rect (STAT_BOX_X_POS+(STAT_BOX_WIDTH/4), STAT_BOX_Y_POS, STAT_BOX_WIDTH/2, STAT_BOX_HEIGHT*2),
				         this.TerrainName + "\n" + this.ResourceType +  " " + this.ResourceValue, mySkin.GetStyle("Box"));
			}
			
			//if(this.controller != null) {
			//	GUI.color = this.controller.getColor();
			//	GUI.Box (new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS + STAT_BOX_OFFSET, STAT_BOX_WIDTH, STAT_BOX_HEIGHT),
			//	         "Owner: " + this.controller.getPlayerID(), mySkin.GetStyle("Box"));
			//}
			
		}
	}

	//Called when tile is pressed
	public void OnMouseUpAsButton() {
//		Debug.Log ("clicked");
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
				else if(this.unitOnTile == null && NewGameController.selectedUnit != null && !canMoveTo)
				{
					NewGameController.deselectAllUnits();
				}
			}
			else if(NewGameController.selectedUnit == null) {
				if (this.unitOnTile != null && NewGameController.currentPlayer == this.unitOnTile.controller) {
					this.unitOnTile.selected ();
				} 
				else if(this.unitOnTile == null && this.isStructure && this.controller == NewGameController.currentPlayer) {
					NewGameController.deselectAllUnits();
					gameObject.GetComponent<UnitBuilding>().enabled = true;
					buildingSelected(NewGameController.currentPlayer.playerID);
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
		if(isEnabled)
		{
			if(this.unitOnTile != null && this.isStructure
			   && NewGameController.selectedUnit == null)
			{
				if(!gameObject.GetComponent<UnitBuilding>().Equals(null))
				{
					gameObject.GetComponent<UnitBuilding>().enabled = false;
					isEnabled = false;
				}
			}
		}
		if(!isEnabled)
		{
			gameObject.GetComponent<UnitBuilding>().enabled = true;
			isEnabled = true;
		}
		if (this.unitOnTile != null) {
			tempModsUpdate ();
		}
		
	}

	protected virtual void tempModsUpdate()
	{

	}

	public virtual void buildingSelected(int val)
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