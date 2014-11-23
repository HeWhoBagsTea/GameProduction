using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour {
	private int progress = 0;

	void OnGUI(){

		GUI.skin.box.wordWrap = true;
		if (progress == 0) {
			GUI.Box(new Rect(25 , 100, 200, 100), "Welcome to Daren's Siege!");
			if(GUI.Button( new Rect( 100 , 200, 100, 35), "Next")) {
				progress++;
			}
		}

		if (progress == 1) {
			GUI.Box(new Rect(25 , 100, 200, 100), "1");
			if(GUI.Button( new Rect( 100 , 200, 100, 35), "Next")) {
				progress++;
			}
		}

		if (progress == 2) {
			GUI.Box(new Rect(25 , 100, 200, 100), "2");
			if(GUI.Button( new Rect( 100 , 200, 100, 35), "Next")) {
				progress++;
			}
		}

		if (progress == 3) {
			GUI.Box(new Rect(25 , 100, 200, 100), "3");
			if(GUI.Button( new Rect( 100 , 200, 100, 35), "Next")) {
				progress++;
			}
		}

		if (progress == 4) {
			GUI.Box(new Rect(25 , 100, 200, 100), "4");
			if(GUI.Button( new Rect( 100 , 200, 100, 35), "Next")) {
				progress++;
			}
		}

		if (progress == 5) {
			GUI.Box(new Rect(25 , 100, 200, 100), "5");
			if(GUI.Button( new Rect( 100 , 200, 100, 35), "Next")) {
				progress++;
			}
		}

		if (progress == 6) {
			GUI.Box(new Rect(25 , 100, 200, 100), "6");
			if(GUI.Button( new Rect( 100 , 200, 100, 35), "Next")) {
				progress++;
			}
		}
	}
}
