using UnityEngine;
using System.Collections;

public class SkyBoxGen : MonoBehaviour {
	public Material[] skyboxMats;
	private int skyNum;

	// Use this for initialization
	void Start () 
	{
		skyNum = Random.Range (0, 3);
		if (skyNum == 0) 
		{
			RenderSettings.skybox = skyboxMats[0];
		};
		if (skyNum == 1) 
		{
			RenderSettings.skybox = skyboxMats[1];
		};
		if (skyNum == 2) 
		{
			RenderSettings.skybox = skyboxMats[2];
		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
