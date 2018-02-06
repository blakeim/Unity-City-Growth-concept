using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
A node to represent a generic building, can be instantiated
as any type of building, allowing buildings to be classed at 
runtime, rather than having nodes be placed as a specific type
of building. I normally would do this with just an abstract class
as the data-type, but Unity doesn't seem to like that idea, figuring
it out as I go */
public class GenericBuildingNode : BuildingNode {

	//This is not the best way to do this, but it will get the point across
	BuildingNode concreteImplementation;

	[SerializeField]
	int buildingTypes;
	
	// Use this for initialization
	void Start () {
		print("Locatoin \t" + this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool build(){

		/*TODO
		scan the area around the node for other buildings, 
		and weight the random number based on their type.
		The more buildings of one type around a new node, 
		the more likely it is to instantiate as that type
		 */
 
		System.Random rnd = new System.Random();

		switch(rnd.Next(buildingTypes)){
		  case 0:
			  concreteImplementation = new ResidentialBuildingNode(this.GetComponent<Transform>().position);
              break;
          case 1:
		  	  concreteImplementation = new CommercialBuildingNode(this.GetComponent<Transform>().position);
              break;
          default:
		  	  print("No corosponding building type");
			  return false;	
		}

		concreteImplementation.build();
		constructed = true;
		return true;
	}

	public override bool expand(){

		return false;
	}

	public override BuildingClass getBuildingClass(){

		return concreteImplementation.getBuildingClass();
	}

	private int[] buildingWeights(){

		int[] weightArray = new int[buildingTypes];

		/*this loop iterates through the array of weights, that starts empty, and see if any buildings
		of the corosponding class are found within a set distance. I really hate this. I 
		hate how not extendable this is, it makes my soul hurt a little bit, but I want to
		try to proof out this concept, I'll refine it if it works. */
		foreach(int i in weightArray){
			
		}

		return weightArray;
	}

	private int checkBuildingsAround(BuildingClass classToCheck){

		RaycastHit hit;

        Vector3 p1 = transform.position;
        float distanceToObstacle = 0;

		/*throw out a sphere, and compare the hits building class to the class given as part of the check call
		Calculate a weight (that maybe should be a float?) and return it to the caller. Those weights will be 
		used to fudge the random number in the direction of the building that should be spawned*/	
        if (Physics.SphereCast(p1, transform.localScale.y, transform.forward, out hit, 10))
        {
            distanceToObstacle = hit.distance;
        }

		return 0;
	}
}
