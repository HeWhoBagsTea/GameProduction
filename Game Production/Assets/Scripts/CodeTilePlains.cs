using UnityEngine;
using System.Collections;

public class CodeTilePlains : MonoBehaviour {

	public Material[] controlRingColors;
	public Material[] tileHighlight;	

	public int moveCost;

	private int controller = -1;

	// Use this for initialization
	void Start () {
		string temp = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material.name;
		for (int i = 0; i < 3; i++) {
			if(temp.Substring(0,temp.IndexOf(" (")) == controlRingColors[i].name) {
				controller = i;
			}
		}
	}

	public void selected(int highLight) {
		MeshRenderer planeRenderer = transform.FindChild ("TerrainPlains").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = tileHighlight[highLight];
	}

	public void highlightWithin(int radius) {
		//radius = radius * 2;
		if (radius > 0) {
			Collider[] hitColliders = Physics.OverlapSphere (transform.position, 2);
			int i = 0;

			while (i < hitColliders.Length) {
				CodeTilePlains temp = hitColliders [i].GetComponentInParent<CodeTilePlains> ();
				if (radius - temp.moveCost >= 0) {
						temp.selected (2);
						//Debug.Log(temp.moveCost);
						temp.highlightWithin(radius - temp.moveCost);
				}
				i++;
			}
		}
	}

	public void deselect() {
		MeshRenderer planeRenderer = transform.FindChild ("TerrainPlains").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = tileHighlight[0];
	}

	public void setControl(int player) {
		MeshRenderer planeRenderer = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = controlRingColors [player];
		controller = player;
	}

}