using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericEdges : MonoBehaviour {

	private GameObject arrow;
	private GameObject obj = null;
	private Vector3 init = new Vector3(0,0);
    private bool useMouse = true;
    public GameObject debugToken;
    private GameObject debugTool = null;
    private GameObject debugToolStart = null;

    // Use this for initialization
    void Start () {
		arrow = GameObject.Find ("Arrow");
    }

	// Update is called once per frame
	void Update () {
        // Draw Arcs with mouse for debugging 
        if (useMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                debugTool = Instantiate(debugToken, transform);
                debugToolStart = Instantiate(debugToken, transform);
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
                    newConfigureArrow(obj, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Destroy(debugTool);
                Destroy(debugToolStart);
                configureArrow(init, init);
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
					//configureArrow (getAppropriatePosition (obj, touchPos.x), touchPos);
                    newConfigureArrow(obj, touchPos);
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
        }

        // Cool objects for start && end
        debugToolStart.transform.position = sourcePosition;
        debugTool.transform.position = destinationPosition;

        // position, rotation & scale
        arrow.transform.position = sourcePosition + (destinationPosition - sourcePosition) / 2;
        arrow.transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.back);
        float distance = Vector3.Distance(sourcePosition, destinationPosition);
        arrow.transform.localScale = new Vector3(distance/2, arrow.transform.localScale.y,0);
    }
}
