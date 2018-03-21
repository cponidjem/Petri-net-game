using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Positions {
	public Vector3 position1;
	public Vector3 position2;
}

public class GenericEdges : MonoBehaviour {
	public class Edge {
		public GameObject start;
		public GameObject end;
	}


	private GameObject arrow;
	private Vector3 init = new Vector3(0,0);
	private GameObject[] placesArray;
	private GameObject[] transitionsArray;
	private List<Edge> edgeList;

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find ("Arrow");
		placesArray = GameObject.FindGameObjectsWithTag ("Place");
		transitionsArray = GameObject.FindGameObjectsWithTag ("Transition");
		edgeList = new List<Edge>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
			bool found = false;
			int i;
			Edge edge = new Edge();
			Positions positions;
			switch (touch.phase)
			{

			//touch begun
			case TouchPhase.Began:
				i = 0;
				while (!found && i < placesArray.Length) {
					if (pointOverlapsWithObject (touchPos, placesArray[i])) {
						edge.start = placesArray[i];
						edgeList.Add (edge);
						found = true;
					}
					i++;
				}
				i = 0;
				while (!found && i < transitionsArray.Length) {
					if (pointOverlapsWithObject (touchPos, transitionsArray[i])) {
						edge.start = transitionsArray[i];
						edgeList.Add (edge);
						found = true;
					}
					i++;
				}
				break;

			//touch moving
			case TouchPhase.Moved:
				//display the moving line
				if(edgeList.Count != 0 && edgeList[edgeList.Count-1].start!=null && edgeList[edgeList.Count-1].end==null) {
					configureArrow (arrow, getAppropriatePosition (edgeList [edgeList.Count - 1].start, touchPos.x), touchPos);
				}
				break;

			//touch ended
			case TouchPhase.Ended:
				i = 0;
				if (edgeList.Count != 0) {
					while (!found && i < placesArray.Length) {
						if (pointOverlapsWithObject (touchPos, placesArray [i]) && edgeList[edgeList.Count-1].start.CompareTag ("Transition")) {
							edgeList[edgeList.Count-1].end = placesArray [i];
							found = true;
						}
						i++;
					}
					i = 0;
					while (!found && i < transitionsArray.Length) {
						if (pointOverlapsWithObject (touchPos, transitionsArray [i]) && edgeList[edgeList.Count-1].start.CompareTag ("Place")) {
							edgeList[edgeList.Count-1].end = transitionsArray [i];
							found = true;
						}
						i++;
					}
					if (found) {
						edge = edgeList[edgeList.Count-1];
						positions = getAppropriatePositions(edge.start,edge.end);
						configureArrow (GameObject.Instantiate(arrow),positions.position1,positions.position2);
					} else {
						if (edgeList [edgeList.Count - 1].end == null) {
							edgeList.RemoveAt (edgeList.Count - 1);
						}
					}
					configureArrow (arrow,init,init);
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

	private Vector3 getAppropriatePosition(GameObject start, float endPosX) {
		Vector3 startPos = start.transform.position;
		Vector3 startScale = start.transform.localScale;
		if (startPos.x < endPosX) {
			return new Vector3 (startPos.x + startScale.x / 2, startPos.y);
		} else {
			return new Vector3 (startPos.x - startScale.x / 2, startPos.y);
		}
	}

	private  Positions getAppropriatePositions(GameObject start, GameObject end) {
		Positions positions;
		Vector3 startPos = start.transform.position;
		Vector3 startScale = start.transform.localScale;
		Vector3 endPos = end.transform.position;
		Vector3 endScale = end.transform.localScale;
		if (startPos.x < endPos.x) {
			positions.position1 = new Vector3 (startPos.x + startScale.x / 2, startPos.y);
			positions.position2 = new Vector3 (endPos.x - endScale.x / 2, endPos.y);
		} else {
			positions.position1 = new Vector3 (startPos.x - startScale.x / 2, startPos.y);
			positions.position2 = new Vector3 (endPos.x + endScale.x / 2, endPos.y);
		}
		return positions;
	}

	private void configureArrow(GameObject instance, Vector3 start, Vector3 end){
		Vector3 position = (start+end)/2;
		float scaleX = Mathf.Sqrt (Mathf.Pow(end.y-start.y,2)+Mathf.Pow(end.x-start.x,2));
		float rotationZ = Mathf.Rad2Deg * Mathf.Acos ((end.x-start.x)/scaleX);
		if (start.y >end.y) {
			rotationZ = - rotationZ;
		}
		instance.transform.position = position;
		instance.transform.localScale = new Vector3(scaleX/2, instance.transform.localScale.y, 0);
		instance.transform.rotation = Quaternion.AngleAxis (rotationZ, Vector3.forward);
	}
}
