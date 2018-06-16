using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private CitySeed city;

	public Button growthButton;

	public float timing{get; set;}
	public float cropYield{get; set;}
	private bool ticking;
	private int frameAccumulator;
	// Use this for initialization
	void Start () {
		
		Button btn = growthButton.GetComponent<Button>();
		timing = 1;
		cropYield = 1;
        btn.onClick.AddListener(TaskOnClick);
		ticking = false;
	}
	
	// Update is called once per frame
	void Update () {
		frameAccumulator++;

		if(frameAccumulator > timing * (1.0f / Time.deltaTime)){
			StopAllCoroutines();
			StartCoroutine("GrowthTick");
			frameAccumulator = 0;
		}
	}

	IEnumerator GrowthTick(){
		city.rain(cropYield * 10);
		city.expand();
		ticking = false;
		return null;
	}

	private void TaskOnClick(){

		city.rain(10);
		city.expand();
	}
}
