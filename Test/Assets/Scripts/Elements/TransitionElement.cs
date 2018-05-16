using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionElement : GameElement {

	public int id;
	public List<PlaceElement> preconditions;
	public List<int> preconditionCoefficients;
    public List<PlaceElement> postconditions;
    public List<int> postconditionCoefficients;
	public GameObject arcPrefab;
	public GameObject transitionExplosion;

    // for color changes
    private float startTime;
    private float[] startColor = new float[3] { 1f, 1f, 1f };
    private float[] endColor = new float[3] { 1f, 1f, 1f };

    // Create arcs corresponding to given pre/postconditions
    void Start()
	{
        startTime = Time.time;
        ArcElement arc = arcPrefab.GetComponent<ArcElement>();
		arc.transition = this;
		int i = 0;
		foreach (PlaceElement p in preconditions)
		{
			arc.place = p;
			arc.type = ArcElement.ConditionType.PRECONDITION;
			arc.coeff = preconditionCoefficients[i];
			++i;
			Instantiate(arc, this.transform);
		}
		i = 0;
		foreach (PlaceElement p in postconditions)
		{
			arc.place = p;
			arc.type = ArcElement.ConditionType.POSTCONDITION;
			arc.coeff = postconditionCoefficients[i];
			++i;
			Instantiate(arc, this.transform);
		}
	}

	void OnMouseUp(){
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider!=null) {
			if (hit.collider.gameObject.Equals (this.gameObject)) {
				Debug.Log ("Transition clicked.");
				game.controller.OnTransitionClicked (id);
			} else {
				if (hit.collider.gameObject.CompareTag("Place")) {
					//Debug.Log ("call addPostcondition with idTransition: "+this.id+" and idPlace: "+(hit.collider.gameObject.GetComponent<PlaceElement>()).id);
                    game.controller.OnArcWasDrawn(this.id, (hit.collider.gameObject.GetComponent<PlaceElement>()).id, false);

				}
			}

		}
	}

	// Display animation
	public void FireTransition()
	{
		Instantiate(transitionExplosion, transform);
	}

    // Update conditions.


    public void changeConditions(List<Arc> newPreconditions, List<Arc> newPostconditions, PlaceElement[] places)
    {
        // Removing old conditions & coefficients.
        preconditions.Clear();
        postconditions.Clear();
        postconditionCoefficients.Clear();
        preconditionCoefficients.Clear();

        foreach (Arc arc in newPreconditions)
        {
            preconditions.Add(places[arc.idPlace]);
            preconditionCoefficients.Add(arc.coeff);
        }
        foreach (Arc arc in newPostconditions)
        {
            postconditions.Add(places[arc.idPlace]);
            postconditionCoefficients.Add(arc.coeff);
        }
        updateArcObjects();
    }

    // Update arcs
    private void updateArcObjects()
    {
        // Destroy all Arcs connected to the transition element.
        ArcElement[] arcsToDestroy = this.GetComponentsInChildren<ArcElement>();
        for(int idx = 0; idx < arcsToDestroy.GetLength(0); idx++)
        {
            Destroy(arcsToDestroy[idx].gameObject);
        }


        // Create new Arcs.
        ArcElement arc = arcPrefab.GetComponent<ArcElement>();
        arc.transition = this;
        int i = 0;
        foreach (PlaceElement p in preconditions)
        {
            arc.place = p;
            arc.type = ArcElement.ConditionType.PRECONDITION;
            arc.coeff = preconditionCoefficients[i];
            ++i;
            Instantiate(arc, this.transform);
        }
        i = 0;
        foreach (PlaceElement p in postconditions)
        {
            arc.place = p;
            arc.type = ArcElement.ConditionType.POSTCONDITION;
            arc.coeff = postconditionCoefficients[i];
            ++i;
            Instantiate(arc, this.transform);
        }
    }

    // Change color according to status
    public void changeStatusColor(bool newState)
    {
        startTime = Time.time;
        startColor = endColor;
        if (newState)
        {
            endColor = new float[3] { 0f, 1f, 0f };  
        }
        else
        {
            endColor = new float[3] { 1f, 1f, 1f };
        }
    }

    public void Update()
    {
        float duration = 1f;
        float t = (Time.time - startTime) / duration;
        transform.GetComponent<SpriteRenderer>().color = new Color(Mathf.SmoothStep(startColor[0], endColor[0], t), 
            Mathf.SmoothStep(startColor[1], endColor[1], t), Mathf.SmoothStep(startColor[2], endColor[2], t), 1);
    }

}
