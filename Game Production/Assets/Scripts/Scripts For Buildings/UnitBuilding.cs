using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitBuilding : TileStandard {

	//Button Stuff;
	private float BUTTON_X_POS = Screen.width * 0.38f;
	private float BUTTON_Y_POS = Screen.height * 0.575f;
	private float BUTTON_WIDTH = Screen.width/(4);
	private float BUTTON_HEIGHT = Screen.height/10;
	private float BUTTON_SPACING = Screen.height/100 + Screen.height/10;
	public Material[] spaceHighlights;
	public TileStandard BuildTarget;
	

	public int playerControl = -1;
	public bool isSelected = false;

	public bool hasBuilt = false;
	
	//Text Sizing
	private int Text = (int)Screen.height/45;
	
	public GameObject[] units;
	
	override public void init()
	{
		this.TerrainName = "Barracks";
		this.isStructure = true;
	}
	
	override public void buildingSelected(int val) {
		NewGameController.deselectAllUnits ();
		this.isSelected = true;

		this.playerControl = val;
	}
	
	override public void deselectAction() {
		this.isSelected = false;
	}
	
	public void Update()
	{
		BUTTON_X_POS = Screen.width * 0.38f;
		BUTTON_Y_POS = Screen.height * 0.575f;
		BUTTON_WIDTH = Screen.width/(4);
		BUTTON_HEIGHT = Screen.height/10;
		BUTTON_SPACING = Screen.height/100 + Screen.height/10;

	}
	
	public void OnGUI()
	{
		if (!this.enabled) {
				return;
		}
		if (this.GetComponent<SimpleAI> () != null && NewGameController.currentPlayer == this.controller) {
			return;
		}
		GUI.skin.button.fontSize = Text;
		GUI.skin.button.fontStyle = FontStyle.Bold;
		if (entered) {
				STAT_BOX_X_POS = Camera.main.WorldToScreenPoint (this.transform.position).x - (STAT_BOX_WIDTH / 2);
				STAT_BOX_Y_POS = Screen.height - Camera.main.WorldToScreenPoint (this.transform.position).y - 30;

			if (this.ResourceType != "") {
						GUI.Label (new Rect (STAT_BOX_X_POS + (STAT_BOX_WIDTH / 4), STAT_BOX_Y_POS, STAT_BOX_WIDTH / 2, STAT_BOX_HEIGHT * 2),
		      this.TerrainName + "\n" + this.ResourceType + " " + this.ResourceValue, mySkin.GetStyle ("Box"));
				} else {
						GUI.Label (new Rect (STAT_BOX_X_POS + (STAT_BOX_WIDTH / 4), STAT_BOX_Y_POS, STAT_BOX_WIDTH / 2, STAT_BOX_HEIGHT),
		      this.TerrainName, mySkin.GetStyle ("Box"));
				}
		}
		if (isSelected) {
			this.selected();
			changeCurrentHighlight();
		}
		if (isSelected && this.BuildTarget != null) {
			Rect[] buildUnit = new Rect[units.Length];
			//sets up the Rects to go from bottom to top.
			for (int i = buildUnit.Length-1; i >= 0; i--) {
				if (i == buildUnit.Length - 1) {
					buildUnit [i] = new Rect (BUTTON_X_POS, BUTTON_Y_POS - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				} else {
					buildUnit [i] = new Rect (BUTTON_X_POS, buildUnit [i + 1].position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				}
			}
			GUI.Box (new Rect ((Screen.width * 0.37f), 200, (Screen.width / (3.5f)), 400), "", this.mySkin.GetStyle ("Box"));
			if (GUI.Button (new Rect (Screen.width * 0.57f, 550, (Screen.width / (12.5f)), 35), "Close")) {
				this.BuildTarget = null;
				this.isSelected = false;
				this.isSelected = true;
			}
			//Change string to fit name of unit.
			for (int i = 0; i < buildUnit.Length; i++) {
				string cost = resourceCost (units [i].GetComponent<UnitBase> ());
				//GUI.color = (haveEnoughResources (units [i].GetComponent<UnitBase> ())) ? Color.white : Color.gray;
				if (GUI.Button (buildUnit [i], "Build " + units [i].name + cost, this.mySkin.GetStyle ("Button")) && GUI.color == Color.white) {
					if (haveEnoughResources (units [i].GetComponent<UnitBase> ())) {
						this.isSelected = false;
						Vector3 rotate = new Vector3 (0, 0, 0);
						if (this.playerControl == 1)
							rotate = new Vector3 (0, 90, 0);
						else if (this.playerControl == 2)
							rotate = new Vector3 (0, 270, 0);
						GameObject prefab = units [i];
						GameObject instantiate = Instantiate (
							prefab, (this.BuildTarget.transform.position),
							this.BuildTarget.transform.rotation) as GameObject;
						instantiate.GetComponent<UnitBase> ().giveControl (transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material);
						this.isSelected = false;
						instantiate.transform.rotation = Quaternion.Euler (rotate);
						instantiate.GetComponent<UnitBase> ().isDone = true;
						this.BuildTarget = null;
						showBuildSpaces();
						this.isSelected = true;
					}
				}
			}	
		}        
	}
	public void selected () {
		NewGameController.deselectAllUnits ();
		isSelected = true;
		
		if (!hasBuilt) {
			showBuildSpaces();
		}
	}

	public void showBuildSpaces(){
		Collider[] hitCollider = Physics.OverlapSphere(this.transform.position,4);
		List<TileStandard> tiles = new List<TileStandard>();
		
		foreach(Collider i in hitCollider) {
			if(i.GetComponent<TileStandard>() != null && i.GetComponent<TileStandard>().unitOnTile == false) {
				tiles.Add(i.GetComponent<TileStandard>());
			}
		}
		
		foreach(TileStandard i in tiles) {
			if(!i.unitOnTile && i.GetComponent<TileStandard>().isStructure != true ) {
				i.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = this.spaceHighlights[2];
				i.canPlaceUnit = true;
			}
			else{
				i.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = this.spaceHighlights[0];
				i.canPlaceUnit = false;
			}
		}
	}

	//AI BuildSpaceReturn
	public TileStandard AIbuildSpaces(){
		Collider[] hitCollider = Physics.OverlapSphere(this.transform.position,4);
		List<TileStandard> tiles = new List<TileStandard>();
		
		foreach(Collider i in hitCollider) {
			if(i.GetComponent<TileStandard>() != null && i.GetComponent<TileStandard>().unitOnTile == false && i != this) {
				tiles.Add(i.GetComponent<TileStandard>());
			}
		}
		
		foreach(TileStandard i in tiles) {
			if(!i.unitOnTile && i.GetComponent<TileStandard>().isStructure != true ) {
				return i;
			}
		}
		return null;
	}

	public bool haveEnoughResources(UnitBase unitInQuestion)
	{
		bool meetCost = false;
		if (this.controller.FoodPool >= unitInQuestion.foodCost && 
		    this.controller.LumberPool >= unitInQuestion.lumberCost && 
		    this.controller.OrePool >= unitInQuestion.oreCost) {
			meetCost = true;
			this.controller.FoodPool -= unitInQuestion.foodCost;
			this.controller.LumberPool -= unitInQuestion.lumberCost;
			this.controller.OrePool -= unitInQuestion.oreCost;
			}
		//Debug.Log (this.controller.FoodPool);
		//Debug.Log (this.controller.LumberPool);
		//Debug.Log (this.controller.OrePool);
		return meetCost;
	}

	private void expendResources(UnitBase unitInQuestion)
	{

	}

	private string resourceCost(UnitBase unitInQuestion)
	{
		string cost = "";
		if(unitInQuestion.foodCost > 0)
		{
			cost += "\nFood cost: " + unitInQuestion.foodCost;
		}
		if(unitInQuestion.lumberCost > 0)
		{
			if(cost.Length > 0)
			{
				cost += "\n";
			}
			cost += "Lumber cost: " + unitInQuestion.lumberCost;
		}
		if(unitInQuestion.oreCost > 0)
		{
			if(cost.Length > 20)
			{
				cost += "\n";
			}
			else
			{
				cost += "\n";
			}
			cost += "Ore cost: " + unitInQuestion.oreCost;
		}
		cost += "\nUpkeep: " + unitInQuestion.UpkeepCost;
		return cost;
	}

	override public void SelectTarget(TileStandard target)
	{
		this.BuildTarget = target;
		Debug.Log (BuildTarget);
	}

	private void changeCurrentHighlight()
	{
		transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ().material = this.spaceHighlights[1];
	}


	//public void buildUnitOnSpace(TileStandard AvatarTarget)
	//{
	//	Damage = this.attackPow;
	//	Damage -=target.currentSpace.defensiveValue;
	//	target.HPcurr -= Damage;
	//	this.hasActioned = true;
	//	audio.PlayOneShot (attackingSound);
	//	if (target.HPcurr > 0) {
	//		StartCoroutine (hurtSound (target, red));
	//		StartCoroutine (damageTaken (target,Damage));
	//	}
	//	if (this.hasMoved) {
	//		deselect ();
	//	} else {
	//		buffMe ();
	//		selected ();
	//	}
	//}
}
