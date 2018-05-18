using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceButtons : MonoBehaviour {

    void Start () {

        RectTransform rt = GetComponent<RectTransform>();
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        rt.GetLocalCorners(v);
        float width = v[2][0]*2;
        float height = v[2][1]*2;
        float limit = Mathf.Min(new float[] { width, height });

        // Play button
        rt = transform.GetChild(0).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 2 - limit / 30, limit / 15);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width / 2 - limit / 6, limit / 3);

        // Menu button
        rt = transform.GetChild(1).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, limit/24, limit / 12);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, limit/24, limit / 12);

        // SUB PANEL
        rt = transform.GetChild(2).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 2 - height / 4, height / 2);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width / 2 - width / 4, width / 2);

        rt.GetLocalCorners(v);
        width = v[2][0] * 2;
        height = v[2][1] * 2;
        limit = Mathf.Min(new float[]{ width,height});
        // Sub panel button - music
        rt = transform.GetChild(2).GetChild(0).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3 - limit / 10, limit / 5);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width / 4 - limit / 10, limit / 5);

        // Sub panel button - volume
        rt = transform.GetChild(2).GetChild(1).GetComponent<RectTransform>();
        RectTransform volumeRt = rt;
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3 - limit / 10, limit / 5);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 2 * width / 4 - limit / 10, limit / 5);

        // Sub panel button - trash
        rt = transform.GetChild(2).GetChild(2).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3 - limit / 10, limit / 5);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 3 * width / 4 - limit / 10, limit / 5);

        // Sub panel button - exit
        rt = transform.GetChild(2).GetChild(3).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, limit / 10, limit / 10);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, limit / 8, limit / 10);

        // Sub panel button - quit
        rt = transform.GetChild(2).GetChild(4).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 3 * height / 4 - limit / 16, limit / 8);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width / 2 - limit / 4, limit / 2);

        // Sub panel button - volume - slider
        volumeRt.GetLocalCorners(v);
        width = v[2][0] * 2;
        height = v[2][1] * 2;
        rt = transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height + height / 5, height / 4);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
    }

    public void Update()
    {
        Start();
    }
}
