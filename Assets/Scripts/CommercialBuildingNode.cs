using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
concrete implementaiton of building node, representing
a commercial building, such as a store or a pub, for now
this is shown on the overworld map as a red cube */
public class CommercialBuildingNode : BuildingNode {

	// Use this for initialization
	new void Start () {
	
		this.buildingClass = "Commercial";	
	}
	
	// Update is called once per frame
	new void Update () {
		
	}

	public override bool build(){

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        	cube.transform.position = this.transform.position;

		cube.SetActive(true);
		cube.GetComponent<Renderer>().material.color = new Color(1,0,0);
		cube.GetComponent<Transform>().localScale += new Vector3(7,7,7);

		this.setConstructed(true);
		return true;
	}

	public override bool expand(){

		return false;
	}
}
