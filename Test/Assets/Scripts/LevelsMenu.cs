using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour {
	public Sprite locker;


	// Use this for initialization
	void Start () {
		RectTransform rt = GetComponent<RectTransform>();
		Vector3[] v = new Vector3[4];
		rt.GetLocalCorners(v);
		float width = v[2][0] * 2;
		float height = v[2][1] * 2;
		float limit = Mathf.Min(new float[] { width, height });

		// Menu button
		rt = transform.GetChild(1).GetComponent<RectTransform>();
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, limit/16, limit / 8);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, limit/16, limit / 8);

		// SUB PANEL
		rt = transform.GetChild(2).GetComponent<RectTransform>();
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0.1f * height, 0.8f * height);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0.1f * width, 0.8f * width);

		rt.GetLocalCorners(v);
		width = v[2][0] * 2;
		height = v[2][1] * 2;
		limit = Mathf.Min(new float[]{ width,height});
		// Sub panel button - music
		rt = transform.GetChild(2).GetChild(0).GetComponent<RectTransform>();
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3 - limit / 8, limit / 4);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width / 4 - limit / 8, limit / 4);

		// Sub panel button - volume
		rt = transform.GetChild(2).GetChild(1).GetComponent<RectTransform>();
		RectTransform volumeRt = rt;
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3 - limit / 8, limit / 4);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 2 * width / 4 - limit / 8, limit / 4);

		// Sub panel button - trash
		rt = transform.GetChild(2).GetChild(2).GetComponent<RectTransform>();
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3 - limit / 8, limit / 4);
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
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0.4f * height);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height + height / 5, height / 4);

		// Handle size
		transform.GetChild(2).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0.5f*height);
		transform.GetChild(2).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.4f * width);

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
