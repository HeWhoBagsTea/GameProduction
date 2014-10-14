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
					foreach(CodeTilePlains i in FindObjectsOfType(typeof(CodeTilePlains))) {
						i.deselect();
					}

					hitTile.highlightWithin(2);
					hitTile.selected(1);
				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			foreach(CodeTilePlains i in FindObjectsOfType(typeof(CodeTilePlains))) {
				i.deselect();
			}
		}

	
	}

}
