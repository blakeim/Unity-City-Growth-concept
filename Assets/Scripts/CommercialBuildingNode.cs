using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
concrete implementaiton of building node, representing
a commercial building, such as a store or a pub, for now
this is shown on the overworld map as a red cube */
public class CommercialBuildingNode : BuildingNode {

	public CommercialBuildingNode(Vector3 location) : base(location){
		print(location);
	}

	// Use this for initialization
	new void Start () {
	
		this.buildingClass = BuildingClass.Commercial;
	}
	
	// Update is called once per frame
	new void Update () {
		
	}

	public override bool build(){

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        	cube.transform.position = location;

		cube.SetActive(true);
		cube.GetComponent<Renderer>().material.color = new Color(0.9f,0.0f,0.0f);
		cube.GetComponent<Transform>().localScale += new Vector3(7,7,7);

		this.setConstructed(true);
		return true;
	}

	public override bool expand(){

		return false;
	}
}
