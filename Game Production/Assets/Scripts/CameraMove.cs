using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public float speed = 5.0f;
	public int mDelta = 10;

	// Use this for initialization
	void Start () {
		this.transform.position = new Vector3(0, 5, -3);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3(0, 0, 0);
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - mDelta)
		{
			pos = new Vector3(0, 0, (1 * Time.deltaTime * speed));
			this.transform.position += pos;
		}
		if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.mousePosition.y < mDelta)
		{
			pos = new Vector3(0, 0, (1 * Time.deltaTime * speed));
			this.transform.position -= pos;
		}
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.mousePosition.x < mDelta)
		{
			pos = new Vector3((1 * Time.deltaTime * speed), 0, 0);
			this.transform.position -= pos;
		}
		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - mDelta)
		{
			pos = new Vector3((1 * Time.deltaTime * speed), 0, 0);
			this.transform.position += pos;
		}
	}
}
