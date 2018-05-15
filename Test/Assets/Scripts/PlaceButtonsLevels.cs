using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceButtonsLevels : MonoBehaviour {

	private RectTransform rt;
	private RectTransform buttonrt;


	void Start () {
		rt = GetComponent<RectTransform>();
		Vector3[] v = new Vector3[4];
		rt.GetWorldCorners(v);
		float width = v[2][0];
		float height = v[2][1];

		float relativeDistanceFromTop = 10f;
		float relativeDistanceFromLeft = 7f;
		buttonrt = transform.GetChild(0).GetComponent<RectTransform>();
		Debug.Log(buttonrt.name);
		buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height*relativeDistanceFromTop, 60);
		buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * relativeDistanceFromLeft, 70);

		buttonrt = transform.GetChild(2).GetComponent<RectTransform>();
		Debug.Log(buttonrt.name);
		buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height*relativeDistanceFromTop-20, 95);
		buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * relativeDistanceFromLeft+130,105);
	}
}

