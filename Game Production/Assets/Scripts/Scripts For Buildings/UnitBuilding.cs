using UnityEngine;
using System.Collections;

public class UnitBuilding : TileStandard {

	//Button Stuff;
	private float BUTTON_X_POS = Screen.width - (Screen.width / 8);
	private float BUTTON_WIDTH = Screen.width/9;
	private float BUTTON_HEIGHT = Screen.height/20;
	private float BUTTON_SPACING = Screen.height/100 + Screen.height/20;
	public int playerControl = -1;
	private bool isSelected = false;
	
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
		BUTTON_X_POS = Screen.width - (Screen.width / 8);
		BUTTON_WIDTH = Screen.width/9;
		BUTTON_HEIGHT = Screen.height/20;
		BUTTON_SPACING = Screen.height/100 + Screen.height/20;
	}
	
	public void OnGUI()
	{
		if (isSelected) {
			Rect[] buildUnit = new Rect[units.Length];
			//sets up the Rects to go from bottom to top.
			for (int i = buildUnit.Length-1; i >= 0; i--) {
				if (i == buildUnit.Length - 1) {
					buildUnit [i] = new Rect (BUTTON_X_POS, Screen.height - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				} else {
					buildUnit [i] = new Rect (BUTTON_X_POS, buildUnit [i + 1].position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				}
			}
			//Change string to fit name of unit.
			for (int i = 0; i < buildUnit.Length; i++) {
				if (GUI.Button (buildUnit [i], "Build " + units [i].name)) {
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
}
