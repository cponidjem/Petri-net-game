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

	// Create arcs corresponding to given pre/postconditions
	void Start()
	{
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

}
