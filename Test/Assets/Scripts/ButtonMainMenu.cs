using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonMainMenu : MonoBehaviour {
	public GameObject memoryPrefab;
	private bool panelOpened = false;

	private GameObject SubPanel = null;

	void Awake(){
		SubPanel = GameObject.Find("Canvas").transform.Find( "SubPanel" ).gameObject;
	}

	// Use this for initialization
	void Start () {
		MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
		if (memory == null) {
			memory = Instantiate<MemoryScript>(memoryPrefab.GetComponent<MemoryScript>());
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public void TogglePanelOpen(){
		panelOpened = !panelOpened;
		SetSubPanelVisibility();
	}

	private void SetSubPanelVisibility(){
		if (SubPanel != null) {
			SubPanel.SetActive(panelOpened);
		}
	}
}