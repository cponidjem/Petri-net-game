using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionElement : GameElement {

    public int id;
    public PlaceElement[] preconditions;
    public PlaceElement[] postconditions;
    public GameObject arcPrefab;

    // Create arcs corresponding to given pre/postconditions
    void Start()
    {
        ArcElement arc = arcPrefab.GetComponent<ArcElement>();
        arc.transition = this;
        foreach (PlaceElement p in preconditions)
        {
            arc.place = p;
            arc.type = ArcElement.ConditionType.PRECONDITION;
            arc.coeff = 1;
            Instantiate(arc, this.transform);
        }
        foreach (PlaceElement p in postconditions)
        {
            arc.place = p;
            arc.type = ArcElement.ConditionType.POSTCONDITION;
            Instantiate(arc, this.transform);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Transition clicked.");
        game.controller.OnTransitionClicked(id);
    }
}
