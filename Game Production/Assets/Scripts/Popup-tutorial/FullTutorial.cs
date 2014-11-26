using UnityEngine;
using System.Collections;

public class FullTutorial : MonoBehaviour {

	private int progress = 0;
	private Rect popUpPosStart = new Rect((Screen.width/2) - 330, 175, 700, 350);
	private Rect nextButtonPosStart = new Rect((Screen.width/2) - 275, 455, 130, 35);
	public GUISkin mySkin;

	private int xOffset = 0;
	private bool doOnce = false;


	public static bool movedAUnit = false;
	public static bool unitAttack = false;
	public static bool disableEndTurn = true;

	void disableAllForTut()
	{
		GameObject[] tutUnits = GameObject.FindGameObjectsWithTag ("Unit");
     	GameObject[] tutTiles = GameObject.FindGameObjectsWithTag ("Tile");
		foreach(GameObject t in tutTiles)
		{
			if(t.GetComponent<TileStandard>() !=null)
			{
				t.GetComponent<TileStandard>().enabled = false;
			}
		}

		foreach(GameObject u in tutUnits)
		{
			if(u.GetComponent<UnitBase>() !=null)
			{
				u.GetComponent<UnitBase>().enabled = false;
			}
		}
	}

	void enableTilesForTut()
	{
	    GameObject[] tutTiles = GameObject.FindGameObjectsWithTag ("Tile");
		foreach(GameObject t in tutTiles)
		{
			if(t.GetComponent<TileStandard>() !=null)
			{
				t.GetComponent<TileStandard>().enabled = true;
			}
		}
	}

	void enableUnitsForTut()
	{
		GameObject[] tutUnits = GameObject.FindGameObjectsWithTag ("Unit");
		foreach(GameObject u in tutUnits)
		{
			if(u.GetComponent<UnitBase>() !=null)
			{
				u.GetComponent<UnitBase>().enabled = true;
			}
		}
	}

	
	void OnGUI(){

		Rect popUpPos = new Rect (popUpPosStart.position.x - xOffset, popUpPosStart.position.y, popUpPosStart.width, popUpPosStart.height);
		Rect nextButtonPos = new Rect (nextButtonPosStart.position.x - xOffset, nextButtonPosStart.position.y, nextButtonPosStart.width, nextButtonPosStart.height);
		
		GUI.skin.box.wordWrap = true;

		if (progress == 0) {
			disableAllForTut();

			GUI.Box(new Rect(popUpPos), "\n\n\nWelcome to war Commander. \n\nThe BLUE army stands at our borders, we must eliminate \nall of their units to secure this front.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}
		
		if (progress == 1) {
			if(!doOnce) {
				doOnce = true;
				for(int i = 0; i < 5; i++) {
					StartCoroutine(moveToSide());
				}
			}
			enableUnitsForTut();
			GUI.Box(new Rect(popUpPos), "\n\n\n Units are your main source to victory. \nThey will allow you to overcome your opponent's defenses. \n\n Now select one of the four RED melee \nunits in the forward row. \n", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nUNITS", mySkin.GetStyle("Label"));
			if(NewGameController.selectedUnit != null) {
				progress++;
			}
		}
		
		if (progress == 2) {
			enableTilesForTut();
			GUI.Box(popUpPos, "\n\n\n The green highlighted spaces show \nwhere you can move your unit.\n\nNow move the selected unit\n next to the wounded BLUE melee unit", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nMOVEMENT", mySkin.GetStyle("Label"));
			if(movedAUnit) {
				progress++;
			}
		}
		
		if (progress == 3) {
			GUI.Box(new Rect(popUpPos), "\n\n\n The orange highlighted spaces\n indicate your attack range. \n\nNow attack the enemy BLUE unit.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nCOMBAT", mySkin.GetStyle("Label"));
			if(unitAttack) {
				progress++;
			}
		}
		
		if (progress == 4) {
			unitAttack= false;
			GUI.Box(new Rect(popUpPos), "\n\n\n Good, the enemy unit lost \nhealth equal to your Attack Power. \n\nAttack power, along with other useful information \nis displayed in the selected unit's stat block.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nCOMBAT", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}
		
		if (progress == 5) {
			GUI.Box(new Rect(popUpPos), "\n\n\nScroll over the wounded BLUE unit \nto see it's remaining HP. \n\nAnother good attack should end this foe.\nTry using another unit to defeat the BLUE unit.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nCOMBAT", mySkin.GetStyle("Label"));
			if(unitAttack) {
				progress++;
			}
		}
		
		if (progress == 6) {
			GUI.Box(new Rect(popUpPos), "\n\n\nVery good Commander, keep this up \nand we shall win for sure.\n\nThough this war can't be won simply through force.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nCOMBAT", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}

		if( progress == 7) {
			enableTilesForTut();
			GUI.Box(popUpPos, "\n\n\nThey say an army marches on it's stomach.\nThis is very true. Units must eat or starve. \n\nAt the start of each turn, soldiers will eat from their \nFOODPOOL located in the bottom left of the screen.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nUPKEEP", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}

		if( progress == 8) {
			//disableAllForTut();
			//NewGameController.deselectAllUnits();
			GUI.Box(popUpPos, "\n\n\nIf there is too little food, your units will begin to starve, \ntaking a point of damage at the start of each turn. \n\nThis will continue until there is enough food \nto feed all of your troops.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nUPKEEP", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}

		if( progress == 9) {
			GUI.Box(popUpPos, "\n\n\nLuckily a unit has a full belly when it has finished Training.\nSo you do not have to worry this turn. \n\nPlains and Hills provide food each turn, but they must be controlled to do so.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nUPKEEP", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}

		if( progress == 10) {
			GUI.Box(popUpPos, "\n\n\nTile control is shown by the color of ring around it.\n\nRed for the RED army. \nBlue for the BLUE army. \nGrey for unclaimed Territory.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nCONTROL", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}

		if( progress == 11) {
			GUI.Box(popUpPos, "\n\n\nUnits may only capture tiles before they move or attack.\nMeaning you may capture land that \nyou ended your previous turn on.\n\nBy clicking the Middle Mouse Button you may capture a tile your selected unit is standing on.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nCONTROL", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				disableAllForTut();
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}

		if( progress == 12) {
			enableTilesForTut();
			GUI.Box(popUpPos, "\n\n\nNot all Lands offer the same bounty Commander.\n\nBy scrolling over a tile you can see what the Tile is, \nand the avaliable resource, in the upper left corner.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nTERRAIN", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}
		if( progress == 13) {
			GUI.Box(popUpPos, "\n\n\nMountains supply us with ore \nto build swords, shields, and Magical focuses.\n\nForests supply wood for our bows, arrows, and staves. \n\nSome terrains affect units in even more ways, \nexperiment to find out.", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nTERRAIN", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}

			if(GUI.Button( new Rect((nextButtonPos.x + 450), nextButtonPos.y, nextButtonPos.width, nextButtonPos.height), "Skip Details")) {
				progress = 14;
			}
		}
		if( progress == 14) {
			enableTilesForTut();
			enableUnitsForTut();
			disableEndTurn = false;
			GUI.Box(popUpPos, "\nWhen you are done with your turn \npress the end turn button, \nlocated in the bottom left corner, \nto end the Day \n\n\n BEST OF LUCK OUT THERE COMMANDER!", mySkin.GetStyle("Box"));

			if(GUI.Button( new Rect(nextButtonPos), "Close")) {
				progress++;
			}
		}

	}

	private IEnumerator moveToSide() {
		for(int i = 0; i < 120; i++) {

			yield return new WaitForSeconds(.00001f);
			xOffset++;
		}
	}
}
