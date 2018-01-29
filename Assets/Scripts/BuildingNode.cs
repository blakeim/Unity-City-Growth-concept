using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
represents a pottential building in a city, when cities grow 
building nodes will be able to spawn in to any type of building,
the type of building should be determined at runtime based on 
what is around the node.Single buildings will also be able to
grow intelligently, adding floors or rooms or changing their 
exterior, depending on where they are and what their purpose is */
public abstract class BuildingNode : MonoBehaviour {

	[SerializeField]
	protected Vector3 location;
	[SerializeField]
	protected bool constructed;

	protected BuildingClass buildingClass;

	public BuildingNode(){

	}

	public BuildingNode(Vector3 location){
		this.location = location;
	}

	public BuildingNode(float x, float y, float z){
		this.location = new Vector3(x, y, z);
	}

	public Vector3 getLocation(){
		return this.location;
	}

	public void setLocation(Vector3 location){
		this.location = location;
	}

	public virtual BuildingClass getBuildingClass(){
		return this.buildingClass;
	}

	public bool getConstructed(){
		return this.constructed;
	}

	public void setConstructed(bool constructed){
		this.constructed = constructed;
	}

	public void Start(){

	}

	public void Update(){

	}

	public abstract bool build();

	public abstract bool expand();
	
}
