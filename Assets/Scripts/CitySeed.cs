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


	[SerializeField]
	private List<BuildingNode> cityMap;
	[SerializeField]
	private List<CropNode> farms;

	private int gold, population, surplus;

	private int maxWidth, maxDepth, currentWidth, currentDepth;

	private int lastCensusPopulation, lastCensusGold, lastCensusSurplus;

	// Use this for initialization
	void Start () {
		
		foreach(BuildingNode b in cityMap){
			if(b.getConstructed()){
				b.build();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void expand(){
		
		System.Random rnd = new System.Random();

		int goldDiff = gold - lastCensusGold;
		int popDiff = population - lastCensusPopulation;
		int surplusDiff = surplus - lastCensusSurplus; 

		int newBuildings = (popDiff / 100);
		int loopCount = 0;

		while(newBuildings > 0 && loopCount < cityMap.Count){
			BuildingNode b = cityMap[rnd.Next(cityMap.Count)];

			if(!b.getConstructed()){
				b.GetComponent<BuildingNode>();
				b.build();
				print("Building built");
				newBuildings -= 1;
			}

			loopCount += 1;
		}

		lastCensusGold = gold;
		lastCensusPopulation = population;
		lastCensusSurplus = surplus;
	}

	public void addPopulation(int population){
		this.population += population;
	}
}
