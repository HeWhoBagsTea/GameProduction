using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OnGUIButtons : MonoBehaviour
{

	//private float rotateSpeed = 2.5f;
	private Vector3 PoV1 = new Vector3 (30, 270, 0);
	private Vector3 PoV2 = new Vector3 (30, 90, 0);
	private Vector3 player1CamPos = new Vector3 (32, 15, 0.05f);
	private Vector3 player2CamPos = new Vector3 (-35, 15, -0.5f);
	private Quaternion newRot;

	private int cameraView = 1;
	//private Vector3 targetPosition = Vector3(25, -90, 0);

	private float RESOURCE_X_POS = 10;
	private float RESOURCE_WIDTH = Screen.width/9;
	private float RESOURCE_HEIGHT = Screen.height/20;
	private float RESOURCE_SPACING = Screen.height/100 + Screen.height/20;

		
	void Start ()
	{
			decidePlayerPOV ();
	}
		
	void decidePlayerPOV ()
	{
		transform.eulerAngles = (cameraView == 1) ? PoV1 : PoV2;
		transform.position = (cameraView == 1) ? player1CamPos : player2CamPos;
	}

//	void smoothCamSwitch()
//	{
//		if (CodeGameController.playersTurn == 1) 
//		{
//			transform.position = Vector3.Lerp (transform.position, player2CamPos, rotateSpeed * Time.deltaTime);
//			newRot = Quaternion.Euler (PoV2);
//			transform.rotation = Quaternion.Slerp (transform.rotation, newRot, rotateSpeed * Time.deltaTime);
//		}
//		else 
//		{
//			transform.position = Vector3.Lerp (transform.position, player1CamPos, rotateSpeed * Time.deltaTime);
//			newRot = Quaternion.Euler (PoV1);
//			transform.rotation = Quaternion.Slerp (transform.rotation, newRot, rotateSpeed * Time.deltaTime);
//		}
//	}
		
	void OnGUI ()
	{
		Rect endButton = new Rect (RESOURCE_X_POS, Screen.height - RESOURCE_SPACING, RESOURCE_WIDTH, RESOURCE_HEIGHT);
		Rect oreButton = new Rect (RESOURCE_X_POS, endButton.position.y - RESOURCE_SPACING, RESOURCE_WIDTH, RESOURCE_HEIGHT);
		Rect woodButton = new Rect(RESOURCE_X_POS, oreButton.position.y - RESOURCE_SPACING, RESOURCE_WIDTH, RESOURCE_HEIGHT);
		Rect foodButton = new Rect(RESOURCE_X_POS, woodButton.position.y - RESOURCE_SPACING, RESOURCE_WIDTH, RESOURCE_HEIGHT);
		
		GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		GUI.Label (foodButton, "FoodPool " + NewGameController.currentPlayer.FoodPool);
		GUI.Label (woodButton, "LumberPool " + NewGameController.currentPlayer.LumberPool);
		GUI.Label (oreButton, "OrePool " + NewGameController.currentPlayer.OrePool);

		if (GUI.Button (endButton, "End Turn")) 
		{
			foreach(GameObject t in tiles)
			{
				if(t.GetComponent<TileStandard>() !=null)
				{
					t.GetComponent<TileStandard>().resolveTurn();
				}
			}

			foreach (GameObject u in units) 
			{
				if(u.GetComponent<UnitBase>() != null) {
					u.GetComponent<UnitBase> ().resolveTurn ();
				}
			}

//			Debug.Log("reached tile resolve end");
			nextPlayer();
			cameraView = (NewGameController.currentPlayer.playerID == 1) ? 1 : 2;    
			decidePlayerPOV ();
			//smoothCamSwitch ();
			
			

//			Debug.Log ("You have ended your turn.");
		}
	}

	void update ()
	{
	
	}

	private void nextPlayer() {
		bool isNext = false;
		int index = 0;

		List<Player> players = new List<Player>();
		GameObject[] playersObjs = GameObject.FindGameObjectsWithTag ("Player"); 
		foreach (GameObject i in playersObjs) {
			if(i.GetComponent<Player>() != null) {
				players.Add(i.GetComponent<Player>());
			}
		}
		
		while(!isNext) {
			if(NewGameController.currentPlayer.getPlayerID() + 1 == players[index].getPlayerID()) {
				NewGameController.currentPlayer = players[index];
				isNext = true;
			}
			else if(NewGameController.currentPlayer.playerID >= players.Count && players[index].playerID == 1) {
				NewGameController.currentPlayer = players[index];
				isNext = true;
			}
			
			index++;
		}
	}
}