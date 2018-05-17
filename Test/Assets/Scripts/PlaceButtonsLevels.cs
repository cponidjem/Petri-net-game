using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceButtonsLevels : MonoBehaviour {



	void Start () {
        RectTransform rt = GetComponent<RectTransform>();
		Vector3[] v = new Vector3[4];
        rt.GetLocalCorners(v);
        float width = v[2][0] * 2;
        float height = v[2][1] * 2;
        float limit = Mathf.Min(new float[] { width, height });

        // Menu button
        rt = transform.GetChild(1).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, limit / 20, limit / 10);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, limit / 20, limit / 10);

        // Reset button
        rt = transform.GetChild(2).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, limit / 20, limit / 10);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, limit / 20, limit / 10);

        // Help button
        rt = transform.GetChild(3).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, limit / 20, limit / 10);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, limit / 20, limit / 10);

    }
}

