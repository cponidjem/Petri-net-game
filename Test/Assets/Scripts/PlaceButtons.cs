using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceButtons : MonoBehaviour {

    private RectTransform rt;
    private RectTransform buttonrt;


    void Start () {
        rt = GetComponent<RectTransform>();
        Debug.Log(rt.name);
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        float width = v[2][0];
        float height = v[2][1];

        float relativeDistanceFromTop = 0.1f;
        float relativeDistanceFromLeft = 0.1f;
        buttonrt = transform.GetChild(1).GetComponent<RectTransform>();
        Debug.Log(buttonrt.name);
        buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height*relativeDistanceFromTop, 50);
        buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * relativeDistanceFromLeft, 50);

        buttonrt = transform.GetChild(0).GetComponent<RectTransform>();
        Debug.Log(buttonrt.name);
        buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height/2, height/10);
        buttonrt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width/2-width/10, width/5);
    }
}
