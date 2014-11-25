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

			GUI.Box(new Rect(popUpPos), "\n\n\n The main objective is to eliminate \nall of your opponent's units.", mySkin.GetStyle("Box"));
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
			GUI.Box(new Rect(popUpPos), "\n\n\n ", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nCOMBAT", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}
		
		if (progress == 5) {
			GUI.Box(new Rect(popUpPos), "\n\n\n ", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}
		
		if (progress == 6) {
			GUI.Box(new Rect(popUpPos), "\n\n\n ", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}

		if( progress == 7) {
			enableTilesForTut();
			GUI.Box(popUpPos, "\n\n\n ", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(movedAUnit) {
				progress++;
			}
		}

		if( progress == 8) {
			disableAllForTut();
			enableTilesForTut();
			NewGameController.deselectAllUnits();
			GUI.Box(popUpPos, "\n\n\n ", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}

		if( progress == 9) {
			GUI.Box(popUpPos, "\n\n\n ", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}

		if( progress == 10) {
			GUI.Box(popUpPos, "\n\n\n ", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}

		if( progress == 11) {
			GUI.Box(popUpPos, "\n\n\n", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}

		if( progress == 12) {
			disableAllForTut();
			GUI.Box(popUpPos, "\n\n\n", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
			if(GUI.Button( new Rect(nextButtonPos), "Next")) {
				progress++;
			}
		}

		if( progress == 13) {
			enableTilesForTut();
			enableUnitsForTut();
			disableEndTurn = false;
			GUI.Box(popUpPos, "\nWhen you are done with your turn press the end turn button located in the bottom left corner. \n\n\n BEST OF LUCK!", mySkin.GetStyle("Box"));
			GUI.Label(popUpPos, "\nWelcome to Daren's Siege!", mySkin.GetStyle("Label"));
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
