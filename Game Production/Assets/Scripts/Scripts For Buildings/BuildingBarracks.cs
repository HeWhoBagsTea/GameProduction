using UnityEngine;
using System.Collections;

public class BuildingBarracks : TileStandard {

	//Button Stuff;
	private float BUTTON_X_POS = Screen.width - (Screen.width / 8);
	private float BUTTON_WIDTH = Screen.width/9;
	private float BUTTON_HEIGHT = Screen.height/20;
	private float BUTTON_SPACING = Screen.height/100 + Screen.height/20;
	public int numOfUnitsToBuild = 1;
	private bool isSelected = false;

	public GameObject[] units;

	public bool hasBuilt;

	override public void init()
	{
		this.hasBuilt = false;
		this.isStructure = true;
	}

	override public void buildingSelected() {
		this.isSelected = true;
	}

	override public void deselectAction() {
		this.isSelected = false;
		this.enabled = false;
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
		if(isSelected){
			Rect[] buildUnit = new Rect[units.Length];

			//sets up the Rects to go from bottom to top.
			for(int i = buildUnit.Length-1; i >= 0; i--)
			{
				if(i == buildUnit.Length-1)
				{
					buildUnit[i] = new Rect(BUTTON_X_POS, Screen.height - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				}
				else
				{
					buildUnit[i] = new Rect(BUTTON_X_POS, buildUnit[i+1].position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
				}
			}
			if(hasBuilt == false){
				//Change string to fit name of unit.
				for(int i = 0; i < buildUnit.Length; i++)
				{
					if(GUI.Button(buildUnit[i], "Build " + units[i].name))
					{
						Debug.Log("Before: " + this.transform.position);
						// change units[i] to find prefab by name?
						GameObject prefab = units[i];
						GameObject instantiate = Instantiate(
							prefab, 
							(this.transform.position),
							this.transform.rotation) as GameObject;
						
						instantiate.GetComponent<UnitBase>().giveControl(transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material);
						this.isSelected = false;

						instantiate.GetComponent<UnitBase>().isDone = true;
						//Uncomment this line out after finding a solution to how ending the turn should act.
						//this.hasBuilt = true; 
						Debug.Log("After: " + instantiate.transform.position);
					}
				}
			}
		}
	}

//	public void resolveTurn()
//	{
//		this.hasBuilt = false;
//		this.isSelected = false;
//		Debug.Log ("HERE");
//	}
}
