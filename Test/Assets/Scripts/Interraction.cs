using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interraction : MonoBehaviour {

	//public variables
	public LineRenderer myLine;
	public Camera camera;

	//private variables
	private Vector3 init = new Vector3(0,0);
	private GameObject place;
	private GameObject transition;

	//Start function : Called before the 1st update when the script is loaded, if the script component is enable
	public void Start() {
		place = GameObject.Find("Place1");
		transition = GameObject.Find ("Transition1");
	}

	//Update function : Called once in every frame
	public void Update() {		
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPos = camera.ScreenToWorldPoint(touch.position);
			switch (touch.phase)
			{
				//touch begun
				case TouchPhase.Began:
					//lock the starting point
					if (pointOverlapsWithObject (touchPos, place)) {
						myLine.SetPosition (0, place.transform.position);	
						myLine.SetPosition (1, place.transform.position);	
					} else if (pointOverlapsWithObject (touchPos, transition)) {
						myLine.SetPosition (0, transition.transform.position);
						myLine.SetPosition (1, transition.transform.position);
					} else {
						myLine.SetPosition (0, init);
						myLine.SetPosition (1, init);
					}
					break;

				//touch moving
				case TouchPhase.Moved:
					//display the moving line
					if (!myLine.GetPosition(0).Equals(init)) {
						myLine.SetPosition(1, touchPos);
					}
					break;

				//touch ended
				case TouchPhase.Ended:
					//lock target point
					if (lineStartedFromObject(place) && pointOverlapsWithObject(touchPos,transition)) {
						myLine.SetPosition (1, transition.transform.position);
					} else if (lineStartedFromObject(transition) && pointOverlapsWithObject(touchPos,place)) {
						myLine.SetPosition (1, place.transform.position);
					} else {
							myLine.SetPosition(0, init);
							myLine.SetPosition(1, init);
						}
					break;
			}
		}
	}

	private bool pointOverlapsWithObject(Vector3 point, GameObject obj) {
		Vector3 objPos = obj.transform.position;
		Vector3 objScale = obj.transform.localScale;

		if (point.x <= objPos.x + objScale.x / 2 && point.x >= objPos.x - objScale.x / 2 && point.y <= objPos.y + objScale.y / 2 && point.y >= objPos.y - objScale.y / 2) {
			return true;
		} else {
			return false;
		}
	}

	private bool lineStartedFromObject(GameObject obj) {
		return myLine.GetPosition (0).Equals (obj.transform.position);
	}
}
