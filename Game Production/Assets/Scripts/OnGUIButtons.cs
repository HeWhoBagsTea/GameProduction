using UnityEngine;
using System.Collections;

public class OnGUIButtons : MonoBehaviour
{

		//private float rotateSpeed = 5;
		private Vector3 PoV1 = new Vector3 (30, 270, 0);
		private Vector3 PoV2 = new Vector3 (30, 90, 0);
		//private Vector3 targetPosition = Vector3(25, -90, 0);
		
		void Start ()
		{
			decidePlayerPOV ();
		}
		
		void decidePlayerPOV ()
		{
			transform.eulerAngles = (CodeGameController.playersTurn == 1) ? PoV1 : PoV2;
			transform.position = (CodeGameController.playersTurn == 1) ? new Vector3 (20, 15, 0.5f) : new Vector3 (-20, 15, -0.5f);
		}
		
		
		void OnGUI ()
		{
				GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
			
				if (GUI.Button (new Rect (20, 40, 80, 20), "End Turn")) 
				{
					foreach (GameObject u in units) 
					{
						u.GetComponent<UnitBase> ().resolveTurn ();
					}
					CodeGameController.playersTurn = (CodeGameController.playersTurn == 1) ? 2 : 1;    
					decidePlayerPOV ();
				
					Debug.Log ("You have ended your turn.");
				}
		}

		void update ()
		{

		}

}