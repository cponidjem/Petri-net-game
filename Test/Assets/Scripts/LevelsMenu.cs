using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour {
	public Sprite locker;


	// Use this for initialization
	void Start () {
		MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
		GameObject holder = GameObject.Find("LevelButtons");
		Button[] levelButtons = holder.GetComponentsInChildren<Button> ();
		//GameObject[] levelButtons = GameObject.FindGameObjectsWithTag ("LevelButton");
		Debug.Log (memory);
		int lastLevelCompleted = memory.getLastLevelCompleted ();
		Debug.Log (lastLevelCompleted);
		for (int i = 0; i < levelButtons.Length; i++) {
			if (i+1 > lastLevelCompleted+1) {
				levelButtons[i].GetComponent<Image> ().sprite = locker;
				levelButtons [i].interactable = false;
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
