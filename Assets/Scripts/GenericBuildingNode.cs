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

	BuildingClass[] buildingClasses;

	// Use this for initialization
	void Start () {
		print("Locatoin \t" + this.transform.position);
		buildingClasses = (BuildingClass[])System.Enum.GetValues(typeof(BuildingClass));
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
		float[] weightArray = buildingWeights();
		System.Array.Sort(weightArray);

		//this figure isn't right, its not right at all. I need something that will make the calculation biased towards the 
		//specific class with the highest weight. But I also need something to compile right now before I go to sleep
		switch((int)System.Math.Ceiling(rnd.Next(buildingClasses.Length) + weightArray[0] * rnd.Next(11))){
		  case (int)BuildingClass.Residential:
			  concreteImplementation = new ResidentialBuildingNode(this.GetComponent<Transform>().position);
              break;
          case (int)BuildingClass.Commercial:
		  	  concreteImplementation = new CommercialBuildingNode(this.GetComponent<Transform>().position);
              break;
          default:
		  	  print("No corosponding building type");
			  return false;	
		}

		concreteImplementation.build();
		constructed = true;
		this.GetComponent<BoxCollider>().enabled = true;
		return true;
	}

	public override bool expand(){

		return false;
	}

	public override BuildingClass getBuildingClass(){

		return concreteImplementation.getBuildingClass();
	}

	private float[] buildingWeights(){

		float[] weightArray = new float[buildingClasses.Length];

		/*this loop iterates through the array of weights, that starts empty, and see if any buildings
		of the corosponding class are found within a set distance. I really hate this. I 
		hate how not extendable this is, it makes my soul hurt a little bit, but I want to
		try to proof out this concept, I'll refine it if it works. */
		for(int i = 0; i < weightArray.Length; i++){
			weightArray[i] = checkBuildingsAround(buildingClasses[i]);
		}

		return weightArray;
	}

	private float checkBuildingsAround(BuildingClass classToCheck){

		RaycastHit[] hits;

        Vector3 p1 = transform.position;
        float weight = 0;

		/*throw out a sphere, and compare the hits building class to the class given as part of the check call
		Calculate a weight (that maybe should be a float?) and return it to the caller. Those weights will be 
		used to fudge the random number in the direction of the building that should be spawned*/	
        if ((hits = Physics.SphereCastAll(p1, transform.localScale.y * 3, transform.forward)) != null)
        {
			foreach(RaycastHit h in hits){
				if(h.collider.gameObject.GetComponent<BuildingNode>().GetType().Equals(classToCheck)){
					weight += (1.0f / hits.Length) * 0.1f;
				}	
			}
        }

		return weight;
	}
}
