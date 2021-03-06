﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceElement : GameElement {

	public int id;
	public int initialMarking;

	public GameObject numberOfTokensText;
	public GameObject token;
    public bool addTokens = false;
    public GameObject addTokensButton;

	// Add text representing the marking.
	void Start()
	{
        float angleIncrease = (2 * Mathf.PI) / initialMarking;
        float distanceFromCenter = 0.5F;
        for (int i = 0; i < initialMarking; i++)
        {
            GameObject newToken = GameObject.Instantiate(token, transform);
            newToken.transform.position = transform.position;
            if (initialMarking > 1)
            {
                newToken.transform.position += new Vector3(Mathf.Sin(angleIncrease * i) * distanceFromCenter, Mathf.Cos(angleIncrease * i) * distanceFromCenter);
            }
        }
        if (addTokens)
        {
            addTokensButton = Instantiate(addTokensButton, game.GetComponentInChildren<Canvas>().transform.GetChild(0).transform);
            addTokensButton.transform.position = transform.position + 1.35F*Vector3.up;
            addTokensButton.GetComponent<Button>().onClick.AddListener(() => { game.controller.OnAddTokensClicked(this.id); });

        }

		//numberOfTokensText = Instantiate(numberOfTokensText, transform.position,transform.rotation, game.GetComponentInChildren<Canvas>().transform);
		//numberOfTokensText.GetComponent<UnityEngine.UI.Text>().text = marking.ToString();
	}


	// Update the tokens to correspond to marking.
	public void changeMarking(int newMarking){
		foreach (Transform child in this.transform) {
			if (child.tag == "Token") {
				Destroy (child.gameObject);
			}
		}
        float angleIncrease = (2 * Mathf.PI) / newMarking;
        float distanceFromCenter = 0.5F;
        for (int i = 0; i < newMarking; i++)
        {
            GameObject newToken = GameObject.Instantiate(token, transform);
            newToken.transform.position = transform.position;
            if (newMarking > 1)
            {
                newToken.transform.position += new Vector3(Mathf.Sin(angleIncrease * i) * distanceFromCenter, Mathf.Cos(angleIncrease * i) * distanceFromCenter);
            }
        }

	}


	void OnMouseUp(){
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider!=null) {
			if (hit.collider.gameObject.CompareTag("Transition")) {
				//Debug.Log ("call addPrecondition with idTransition: "+(hit.collider.gameObject.GetComponent<TransitionElement>()).id+" and idPlace: "+this.id);
                game.controller.OnArcWasDrawn((hit.collider.gameObject.GetComponent<TransitionElement>()).id, this.id, true);
            }
		}
	}
}
