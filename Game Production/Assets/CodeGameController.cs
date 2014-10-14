using UnityEngine;
using System.Collections;

public class CodeGameController : MonoBehaviour {

	public GameObject tilePlains;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

			if(Physics.Raycast(ray, out hit)) {
				GameObject hitObject = hit.collider.gameObject;
				CodeTilePlains hitTile = hitObject.GetComponent<CodeTilePlains>();
				if(hitTile) {
					hitTile.highlightWithin(2);
					hitTile.selected(1);
				}
			}
		}
	}

}
