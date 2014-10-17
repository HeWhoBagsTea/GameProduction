using UnityEngine;
using System.Collections;

public class OnGUIButtons : MonoBehaviour
{

	//private float rotateSpeed = 2.5f;
	private Vector3 PoV1 = new Vector3 (30, 270, 0);
	private Vector3 PoV2 = new Vector3 (30, 90, 0);
	private Vector3 player1CamPos = new Vector3 (20, 15, 0.05f);
	private Vector3 player2CamPos = new Vector3 (-21, 15, -0.5f);
	private Quaternion newRot;
	//private Vector3 targetPosition = Vector3(25, -90, 0);
		
	void Start ()
	{
			decidePlayerPOV ();
	}
		
	void decidePlayerPOV ()
	{
			transform.eulerAngles = (CodeGameController.playersTurn == 1) ? PoV1 : PoV2;
			transform.position = (CodeGameController.playersTurn == 1) ? player1CamPos : player2CamPos;
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
			GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		
			if (GUI.Button (new Rect (Screen.width * 0.01f, Screen.height * 0.9f, Screen.width * 0.1f, Screen.height * 0.06f), "End Turn")) 
			{
					foreach (GameObject u in units) 
					{
						u.GetComponent<UnitBase> ().resolveTurn ();
					}
					CodeGameController.playersTurn = (CodeGameController.playersTurn == 1) ? 2 : 1;    
					decidePlayerPOV ();
					//smoothCamSwitch ();
					
					

					Debug.Log ("You have ended your turn.");
			}
	}

	void update ()
	{
	
	}
}