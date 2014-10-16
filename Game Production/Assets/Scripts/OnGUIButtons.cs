using UnityEngine;
using System.Collections;

public class OnGUIButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	void OnGUI()
	{
		if (GUI.Button (new Rect (20, 40, 80, 20), "End Turn")) 
		{
			CodeGameController.playersTurn = 2;
			Debug.Log ("You have ended your turn.");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
