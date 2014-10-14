using UnityEngine;
using System.Collections;

public class CodeTilePlains : MonoBehaviour {

	public Material[] controlRingColors;
	public Material[] tileHighlight;	

	private int controller = -1;

	// Use this for initialization
	void Start () {

	}

	public void selected(int highLight) {
		MeshRenderer planeRenderer = transform.FindChild ("TerrainPlains").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = tileHighlight[highLight];
	}

	public void highlightWithin(int radius) {
		radius = radius * 2;
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, radius);
		int i = 0;

		while (i < hitColliders.Length) {
			CodeTilePlains temp = hitColliders[i].GetComponentInParent<CodeTilePlains>();
			temp.selected(2);
			i++;
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