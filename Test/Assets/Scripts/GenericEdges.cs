using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericEdges : MonoBehaviour {

	private GameObject arrow;
	private GameObject obj = null;
	private Vector3 init = new Vector3(0,0);

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find ("Arrow");
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

			switch (touch.phase)
			{

			//touch begun
			case TouchPhase.Began:
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider!=null) {
					if (hit.collider.gameObject.CompareTag("Transition") || hit.collider.gameObject.CompareTag("Place")) {
						obj = hit.collider.gameObject;
					}
				}
				break;

			//touch moving
			case TouchPhase.Moved:
				//display the moving line
				if(obj != null) {
					configureArrow (getAppropriatePosition (obj, touchPos.x), touchPos);
				}
				break;

			//touch ended
			case TouchPhase.Ended:
				configureArrow (init, init);
				obj = null;
				break;
			}
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

	private void configureArrow(Vector3 start, Vector3 end){
		Vector3 position = (start+end)/2;
		float scaleX = Mathf.Sqrt (Mathf.Pow(end.y-start.y,2)+Mathf.Pow(end.x-start.x,2));
		float rotationZ = Mathf.Rad2Deg * Mathf.Acos ((end.x-start.x)/scaleX);
		if (start.y >end.y) {
			rotationZ = - rotationZ;
		}
		arrow.transform.position = position;
		arrow.transform.localScale = new Vector3(scaleX/2, arrow.transform.localScale.y, 0);
		arrow.transform.rotation = Quaternion.AngleAxis (rotationZ, Vector3.forward);
	}
}
