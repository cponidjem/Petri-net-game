using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : GameElement {

    // Return all places of the scene.
    public List<Place> getPlaces()
    {
        List<Place> places = new List<Place>();
        foreach (PlaceElement p in this.GetComponentsInChildren<PlaceElement>())
        {
            places.Add(new Place(p.id, p.marking));
        }
        return places;
    }

    // Return all transitions in the scene.
    public List<Transition> getTransitions()
    {
        List<Transition> transitions = new List<Transition>();
        foreach (TransitionElement t in this.GetComponentsInChildren<TransitionElement>())
        {
            List<Arc> preconditions = new List<Arc>();
            List<Arc> postconditions = new List<Arc>();
            foreach (ArcElement a in this.GetComponentsInChildren<ArcElement>())
            {
                if (a.type == ArcElement.ConditionType.POSTCONDITION)
                {
                    postconditions.Add(new global::Arc(a.place.id, a.coeff));
                }
                else
                {
                    preconditions.Add(new global::Arc(a.place.id, a.coeff));
                }
            }
            transitions.Add(new Transition(t.id, preconditions, postconditions));
        }
        return transitions;
    }


}
