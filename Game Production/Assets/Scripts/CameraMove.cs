using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public float speed = 5.0f;
	public int mDelta = 10;
	public int minX = -20; 
	public int maxX = 20; 
	public int minZ = -20;
	public int maxZ = 20;

	// Use this for initialization
	void Start () {
		//this.transform.position = new Vector3(0, 5, -3);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3 (0, 0, 0);
		
		if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.mousePosition.y <= Screen.height - mDelta) && this.transform.position.x < maxX)
		{
			pos = new Vector3((1 * Time.deltaTime * speed), 0, 0);
			this.transform.position += (CodeGameController.playersTurn == 1) ? pos : -pos;

		}
		if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.mousePosition.y > mDelta) && this.transform.position.x > minX)
		{
			pos = new Vector3((1 * Time.deltaTime * speed),0,0);
			this.transform.position -= (CodeGameController.playersTurn == 1) ? pos : -pos;
		}
		if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.mousePosition.x < mDelta) && this.transform.position.z > minZ)
		{
			pos = new Vector3(0,0,(1 * Time.deltaTime * speed));
			this.transform.position -= (CodeGameController.playersTurn == 1) ? pos : -pos;
		}
		if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - mDelta) && this.transform.position.z < maxZ)
		{
			pos = new Vector3(0,0,(1 * Time.deltaTime * speed));
			this.transform.position += (CodeGameController.playersTurn == 1) ? pos : -pos;
		}
	}
}
