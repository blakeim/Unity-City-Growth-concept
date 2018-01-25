using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private CitySeed city;

	public Button growthButton;

	// Use this for initialization
	void Start () {
		
		Button btn = growthButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void TaskOnClick(){

		city.addPopulation(100);
		city.expand();
	}
}
