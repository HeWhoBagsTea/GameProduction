using UnityEngine;
using System.Collections;

public class RelativePoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(0, 0, 30);
		//transform.position = .InverseTransformDirection(transform.position - cameraTransform.position);
	}
}
