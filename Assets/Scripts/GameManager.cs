using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private CitySeed city;

	public Button growthButton;
	public Slider cropYieldSilder;
	public Slider timingSlider;

	private float timing = 5;
	private float cropYield = 10;
	private bool ticking;

	// Use this for initialization
	void Start () {
		
		Button btn = growthButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
		ticking = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!ticking){
			StopAllCoroutines();
			StartCoroutine("GrowthTick");
		}
	}

	IEnumerator GrowthTick(){
		yield return new WaitForSeconds(timing);
		city.rain(cropYield);
		city.expand();
	}

	private void TaskOnClick(){

		city.rain(10);
		city.expand();
	}
}
