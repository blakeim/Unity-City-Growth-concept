using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
a node representing crops. Each crop node will be able to yield
a certain ammount of food based on their saturation by the rain
or other factors. Farms should be able to expand similarly to 
buildings, think of this as a 2.0 feature */
public abstract class CropNode : MonoBehaviour {

	int maxYield;
	int currentYield;

	int rainSaturation;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
