using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
a node representing crops. Each crop node will be able to yield
a certain ammount of food based on their saturation by the rain
or other factors. Farms should be able to expand similarly to 
buildings, think of this as a 2.0 feature */
public abstract class CropNode : MonoBehaviour {

	[SerializeField]
	protected int maxYield, areaW, areaH;
	protected int currentYield;

	private float rainSaturation;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getCurrentYield(){
		//if rain saturation is over 1, it should produce lower yield, capping at 0% yield for a value of 2
		//I wanna double check this formula, it made sense on the whiteboard, but math is hard
		return (int)(maxYield * Mathf.Abs((rainSaturation - 1) - rainSaturation % 1));
	}

	public void setRainSaturation(float rainSaturation){
		this.rainSaturation = Mathf.Min(2.0f, rainSaturation);
	}
}
