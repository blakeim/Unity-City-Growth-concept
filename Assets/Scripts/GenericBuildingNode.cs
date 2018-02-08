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

	[SerializeField]
	float weight_fudge, proximity_threshhold; //this field will be used to adjust the weighting

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
		SortedDictionary<BuildingClass, float> building_weights = buildingWeights();

		BuildingClass most_frequent = BuildingClass.Residential;
		float weighting_factor = 0.0f;
		float temp_output = 0.0f;
		foreach(BuildingClass b in building_weights.Keys){

			building_weights.TryGetValue(b, out temp_output);
			if(temp_output > weighting_factor){
				weighting_factor = temp_output;
				most_frequent = b;
			}
		}

		switch((rnd.NextDouble() + temp_output >= proximity_threshhold ? (int)most_frequent : rnd.Next(buildingClasses.Length)) ){
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

	private SortedDictionary<BuildingClass, float> buildingWeights(){

		SortedDictionary<BuildingClass, float> building_weights = new SortedDictionary<BuildingClass, float>();

		/*this loop iterates through the array of weights, that starts empty, and see if any buildings
		of the corosponding class are found within a set distance. I really hate this. I 
		hate how not extendable this is, it makes my soul hurt a little bit, but I want to
		try to proof out this concept, I'll refine it if it works. */
		foreach(BuildingClass b in buildingClasses){
			building_weights.Add(b, checkBuildingsAround(b));
		}

		return building_weights;
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
					weight += (1.0f / hits.Length) * weight_fudge;
				}	
			}
        }

		return weight;
	}
}
