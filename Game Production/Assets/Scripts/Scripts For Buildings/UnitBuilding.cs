using UnityEngine;
using System.Collections;

public class UnitBuilding : TileStandard {

	//Button Stuff;
	private float BUTTON_X_POS = Screen.width * 0.38f;
	private float BUTTON_Y_POS = Screen.height * 0.575f;
	private float BUTTON_WIDTH = Screen.width/(4);
	private float BUTTON_HEIGHT = Screen.height/10;
	private float BUTTON_SPACING = Screen.height/100 + Screen.height/10;

	public int playerControl = -1;
	private bool isSelected = false;
	
	//Text Sizing
	private int Text = (int)Screen.height/47;
	
	public GameObject[] units;
	
	override public void init()
	{
		this.isStructure = true;
	}
	
	override public void buildingSelected(int val) {
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

		int Text = (int)Screen.height/47;
	}
	
	public void OnGUI()
	{
		GUI.skin.button.fontSize = Text;
		if (isSelected) {
			Rect[] buildUnit = new Rect[units.Length];
			//sets up the Rects to go from bottom to top.
			for (int i = buildUnit.Length-1; i >= 0; i--) {
				if (i == buildUnit.Length - 1) {
					buildUnit [i] = new Rect (BUTTON_X_POS, BUTTON_Y_POS - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				} else {
					buildUnit [i] = new Rect (BUTTON_X_POS, buildUnit [i + 1].position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				}
			}


			GUI.Box(new Rect((Screen.width * 0.37f), 200, (Screen.width/(3.5f)), 400), "");
			if(GUI.Button(new Rect(Screen.width * 0.57f, 550, (Screen.width/(12.5f)), 35), "Close"))
			{
				this.isSelected = false;
			}

			//Change string to fit name of unit.
			for (int i = 0; i < buildUnit.Length; i++) {
				string cost = resourceCost(units [i].GetComponent<UnitBase> ());
				//GUI.color = (haveEnoughResources (units [i].GetComponent<UnitBase> ())) ? Color.white : Color.gray;
				if (GUI.Button (buildUnit [i], "Build " + units [i].name + cost) && GUI.color == Color.white) {

					if (haveEnoughResources (units [i].GetComponent<UnitBase> ())) {
						Vector3 rotate = new Vector3 (0, 0, 0);
						if (this.playerControl == 1)
							rotate = new Vector3 (0, 90, 0);
						else if (this.playerControl == 2)
							rotate = new Vector3 (0, 270, 0);
						GameObject prefab = units [i];
						GameObject instantiate = Instantiate (
							prefab,
							(this.transform.position),
							this.transform.rotation) as GameObject;
						instantiate.GetComponent<UnitBase> ().giveControl (transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material);
						this.isSelected = false;
						instantiate.transform.rotation = Quaternion.Euler (rotate);
						instantiate.GetComponent<UnitBase> ().isDone = true;
					}
				}
			}

		}
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
				cost += "\t";
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
				cost += "\t";
			}
			cost += "Ore cost: " + unitInQuestion.oreCost;
		}
		if(unitInQuestion.UpkeepCost > 0)
		{
			if(cost.Length > 0)
			{
				cost += "\n";
			}
			cost += "Upkeep cost: " + unitInQuestion.UpkeepCost;
		}
		return cost;
	}
}
