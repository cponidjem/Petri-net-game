using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericEdges : MonoBehaviour {

	private GameObject arrow;
	private GameObject obj = null;
	private Vector3 init = new Vector3(0,0);
    private bool placeType;

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
					if (hit.collider.gameObject.CompareTag("Place")) {
						obj = hit.collider.gameObject;
                        placeType = true;
                    }
                    else if(hit.collider.gameObject.CompareTag("Transition")) {
                        obj = hit.collider.gameObject;
                        placeType = false;
                    }
				}
				break;

			//touch moving
			case TouchPhase.Moved:
				//display the moving line
				if(obj != null) {
					//configureArrow (getAppropriatePosition (obj, touchPos.x), touchPos);
                    newConfigureArrow(obj.transform.position, touchPos);
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
			return new Vector3 (startPos.x + startScale.x / 2, startPos.y, startPos.z);
		} else {
			return new Vector3 (startPos.x - startScale.x / 2, startPos.y, startPos.z);
		}
	}

	private void configureArrow(Vector3 start, Vector3 end){
		Vector3 position = start;
		position.x = (start.x+end.x)/2;
		position.y = (start.y+end.y)/2;
		float scaleX = Mathf.Sqrt (Mathf.Pow(end.y-start.y,2)+Mathf.Pow(end.x-start.x,2));
		float rotationZ = Mathf.Rad2Deg * Mathf.Acos ((end.x-start.x)/scaleX);
		if (start.y >end.y) {
			rotationZ = - rotationZ;
		}
		arrow.transform.position = position;
		arrow.transform.localScale = new Vector3(scaleX/2, arrow.transform.localScale.y, 0);
		arrow.transform.rotation = Quaternion.AngleAxis (rotationZ, Vector3.forward);
	}

    private void newConfigureArrow(Vector3 sourcePosition, Vector3 destinationPosition)
    {
        // Place the arc between place and touchDestination
        float angle = Vector3.SignedAngle(sourcePosition - destinationPosition, Vector3.right, Vector3.forward);
        float angleRad = Mathf.Deg2Rad * angle;
        float r = 1F;
        float transitionWidth = 0.1F;

        // Move sourceDestination according to the type of the source
        if (placeType)
        {
            sourcePosition = sourcePosition + new Vector3(-Mathf.Cos(angleRad) * r, Mathf.Sin(angleRad) * r);
        }
        else
        {
            sourcePosition = sourcePosition +
            new Vector3(transitionWidth * Mathf.Sign(sourcePosition.x - destinationPosition.x), 0);
        }

        arrow.transform.position = new Vector3(
            sourcePosition.x + (destinationPosition.x - sourcePosition.x) / 2,
            sourcePosition.y + (destinationPosition.y - sourcePosition.y) / 2,
            sourcePosition.z + (destinationPosition.z - sourcePosition.z) / 2);

        // Rotate the arc to right angle & according to type
        float z_angle_deg =
            (180 / Mathf.PI) *
            Mathf.Atan(
            ((sourcePosition.y - destinationPosition.y) / 2) /
            ((sourcePosition.x - destinationPosition.x) / 2));

        arrow.transform.rotation = Quaternion.AngleAxis(z_angle_deg, Vector3.forward);
        // Scale the arc according to the distance
        float distance = Vector3.Distance(sourcePosition, destinationPosition);
        float scaleFactor = distance / 2;
        arrow.transform.localScale = new Vector3(distance/2, arrow.transform.localScale.y);
    }
}
