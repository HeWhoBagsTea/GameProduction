using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target != null)
		{


			if(Input.GetKey(KeyCode.Q))
			{
				transform.RotateAround(target.transform.position, Vector3.down, Time.deltaTime * 15);
			}

			if(Input.GetKey(KeyCode.E))
			{
				transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * 15);
			}

			if(Input.GetKey(KeyCode.R))
			{
				transform.RotateAround(target.transform.position, Vector3.back, Time.deltaTime * 15);
			}

			if(Input.GetKey(KeyCode.F))
			{
				transform.RotateAround(target.transform.position, Vector3.forward, Time.deltaTime * 15);
			}
		}
	}
}
