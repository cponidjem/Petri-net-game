using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericEdges : MonoBehaviour {

	public GameObject arrowPrefab;
    public GameObject arrowHeadPrefab;
    public bool useMouse = true;
    private GameObject obj = null;
	private Vector3 init = new Vector3(0,0);
    private GameObject arrow = null;
    private GameObject arrowHead = null;


    // Update is called once per frame
    void Update () {
        // Draw Arcs with mouse for debugging 
        if (useMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                arrow = Instantiate(arrowPrefab, transform);
                arrowHead = Instantiate(arrowHeadPrefab, transform);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Place") || hit.collider.gameObject.CompareTag("Transition"))
                    {
                        obj = hit.collider.gameObject;
                    }
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (obj != null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject.Equals(obj))
                    {
                        //Place arrow to zero
                        arrowHead.transform.position = new Vector3(0, 0, 0);
                        arrow.transform.position = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        newConfigureArrow(obj, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Destroy(arrowHead);
                Destroy(arrow);
                obj = null;
            }
        }
		else if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

			switch (touch.phase)
			{

			//touch begun
			case TouchPhase.Began:
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider!=null) {
					if (hit.collider.gameObject.CompareTag("Place") || hit.collider.gameObject.CompareTag("Transition")) {
						obj = hit.collider.gameObject;
                    }
				}
				break;

			//touch moving
			case TouchPhase.Moved:
				//display the moving line
				if(obj != null) {
                    RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (hit2.collider != null && hit2.collider.gameObject.Equals(obj))
                    {
                        //Place arrow to zero
                        arrowHead.transform.position = new Vector3(0, 0, 0);
                        arrow.transform.position = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        newConfigureArrow(obj, touchPos);
                    }
				}
				break;

			//touch ended
			case TouchPhase.Ended:
				Destroy(arrowHead);
                Destroy(arrow);
				obj = null;
				break;
			}
		}
        // To make sure they are destroyed
        else if (arrow != null)
        {
            Destroy(arrow);
            obj = null;
        }
        else if (arrowHead != null)
        {
            Destroy(arrowHead);
            obj = null;
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


    private void newConfigureArrow(GameObject sourceObject, Vector3 destinationPosition)
    {
        Vector3 sourcePosition = sourceObject.transform.position;
        destinationPosition = destinationPosition + 2*Vector3.forward; // because canvas is +2 in front

        float angle = Vector3.SignedAngle(sourcePosition - destinationPosition, new Vector3(1, 0, 0), new Vector3(0,0,1));
        float angleRad = Mathf.Deg2Rad * angle;
        float placeElementRay = 1F;
        float transitionElementWidth = 0.1F;

        // Move sourceDestination according to the type of the source
        if (sourceObject.CompareTag("Place"))
        {
            sourcePosition += new Vector3(-Mathf.Cos(angleRad), Mathf.Sin(angleRad))* placeElementRay;
        }
        else
        {
            sourcePosition += new Vector3(transitionElementWidth * Mathf.Sign(destinationPosition.x - sourcePosition.x), Mathf.Sin(angleRad));
            // Calculate small change in the angle caused by transitionElementWidth
            angle = Vector3.SignedAngle(sourcePosition - destinationPosition, new Vector3(1, 0, 0), new Vector3(0, 0, 1));
            angleRad = Mathf.Deg2Rad * angle;
        }

        // Cool objects for start && end
        float arrowHeadSize = 0.2f;
        Vector3 arrowHeadPosition = destinationPosition - new Vector3(-Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * arrowHeadSize;
        arrowHead.transform.position = arrowHeadPosition;
        arrowHead.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.back);

        // position, rotation & scale
        arrow.transform.position = sourcePosition + (destinationPosition - sourcePosition) / 2;
        arrow.transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.back);
        float distance = Vector3.Distance(sourcePosition, destinationPosition);
        arrow.transform.localScale = new Vector3(distance/2, arrow.transform.localScale.y,0);
    }
}
