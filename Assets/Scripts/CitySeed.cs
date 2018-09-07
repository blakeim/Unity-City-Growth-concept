using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
represents a city, populated by different types of nodes
The city will grow semi-randomly based on the available 
resources, population increases will cause new buildings to
spawn, where as increases in surplus will grow population as
well as gold, and increases in gold will cause buildings to be
"upgraded" and become more complex. */
public class CitySeed : MonoBehaviour {


	private List<BuildingNode> cityMap;
	private List<CropNode> farms;

	private int gold, population, surplus;

	private int maxWidth, maxDepth, currentWidth, currentDepth;

	private int lastCensusPopulation, lastCensusGold, lastCensusSurplus;

	// Use this for initialization
	void Start () {

		GameObject[] tempBuildings = GameObject.FindGameObjectsWithTag("Building");
		GameObject[] tempCrops = GameObject.FindGameObjectsWithTag("Crop");

		cityMap = new List<BuildingNode>();
		farms = new List<CropNode>();

		foreach(GameObject g in tempBuildings){
			cityMap.Add(g.GetComponent<BuildingNode>());
		}
		foreach(GameObject g in tempCrops){
			farms.Add(g.GetComponent<CropNode>());
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void expand(){
		
		System.Random rnd = new System.Random();

		surplus += harvest();
		int neededSupplies = (population % surplus);
		
		surplus -= neededSupplies;
		gold += surplus;
		population += surplus;

		int goldDiff = gold - lastCensusGold;
		int popDiff = population - lastCensusPopulation;
		int surplusDiff = surplus - lastCensusSurplus; 

		int newBuildings = (popDiff / 100);
		int loopCount = 0;

		while(newBuildings > 0 && loopCount < cityMap.Count){
			BuildingNode b = cityMap[rnd.Next(cityMap.Count)];

			if(b != null && !b.getConstructed()){
				if(b.build()){
					newBuildings -= 1;
				}
			}

			loopCount += 1;
		}

		lastCensusGold = gold;
		lastCensusPopulation = population;
		lastCensusSurplus = surplus;
	}

	public void rain(float saturation){
		foreach(CropNode c in farms){
			c.setRainSaturation(saturation);
		}
	}
	
	public int harvest(){

		int crops = 0;

		foreach(CropNode c in farms){
			crops+= c.getCurrentYield();
		}

		return crops;
	}
}
