using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCropNode : CropNode {

	// Use this for initialization
	void Start () {
		
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = this.transform.position;

		cube.SetActive(true);
		cube.GetComponent<Renderer>().material.color = new Color(0.9f,0.9f,0.0f);
		cube.GetComponent<Transform>().localScale += new Vector3(areaW,7, areaH);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
