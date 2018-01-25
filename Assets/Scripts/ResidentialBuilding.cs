using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
a building node to represent a residential building, like a house.
Residential buildings would have certain layout restrictions, like
requiring bedrooms and sitting rooms and that. Currently, these
are represente on the map by green cubes */
public class ResidentialBuilding : BuildingNode {

	// Use this for initialization
	new void Start () {

		this.buildingClass = "Residential";
	}
	
	// Update is called once per frame
	new void Update () {
		
	}

	public override bool build(){

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        	cube.transform.position = this.transform.position;

		cube.SetActive(true);
		cube.GetComponent<Renderer>().material.color = new Color(0,1,0);
		cube.GetComponent<Transform>().localScale += new Vector3(7,7,7);

		this.setConstructed(true);
		return true;
	}

	public override bool expand(){

		return false;
	}
}
