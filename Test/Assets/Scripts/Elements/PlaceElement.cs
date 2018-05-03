using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceElement : GameElement {

	public int id;
	public int initialMarking;

	public GameObject numberOfTokensText;
	public GameObject token;

	// Add text representing the marking.
	void Start()
	{
		for (int i = 0; i < initialMarking; i++) {
			GameObject newToken = GameObject.Instantiate (token, transform);
		}
		//numberOfTokensText = Instantiate(numberOfTokensText, transform.position,transform.rotation, game.GetComponentInChildren<Canvas>().transform);
		//numberOfTokensText.GetComponent<UnityEngine.UI.Text>().text = marking.ToString();
	}


	// Update the text to correspond to marking.
	public void changeMarking(int newMarking){
		Debug.Log ("Changing Marking");
		foreach (Transform child in this.transform) {
			if (child.tag == "Token") {
				Destroy (child.gameObject);
			}
		}
		for (int i = 0; i < newMarking; i++) {
			GameObject newToken = GameObject.Instantiate (token, transform);
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
