using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place
{
    public int id;
    public int marking;
    public Place(int id, int marking)
    {
        this.id = id;
        this.marking = marking;
    }
}

public class Arc
{
    public int idPlace;
    public int coeff;
    public Arc(int idPlace, int coeff)
    {
        this.idPlace = idPlace;
        this.coeff = coeff;
    }
}

public class Transition
{
    public int id;
    public List<Arc> preconditions;
    public List<Arc> postconditions;

	private void cloneArcsList(ref List<Arc> cloneArcs, List<Arc> arcs){
		Arc temp;
		foreach (Arc arc in arcs) {
			temp = new Arc (arc.idPlace, arc.coeff);
			cloneArcs.Add (temp);
		}
	}

    public Transition(int id, List<Arc> preconditions, List<Arc> postconditions)
    {
        this.id = id;
		this.preconditions = new List<Arc>();
		cloneArcsList (ref this.preconditions, preconditions);
		this.postconditions = new List<Arc>();
		cloneArcsList (ref this.postconditions, postconditions);
    }

    public bool isEnabled(List<Place> places)
    {
        foreach (Arc precondition in preconditions)
        {
            foreach (Place place in places)
            {
                if (precondition.idPlace == place.id)
                {
                    if (precondition.coeff > place.marking)
                    {
                        return false;
                    }
                    break;
                }
            }
        }
        return true;
    }

    public List<Place> fire(List<Place> places)
    {
        foreach (Place place in places)
        {
            foreach (Arc precondition in preconditions)
            {
                if (precondition.idPlace == place.id)
                {
                    place.marking = place.marking - precondition.coeff;
                    break;
                }
            }
            foreach (Arc postcondition in postconditions)
            {
                if (postcondition.idPlace == place.id)
                {
                    place.marking = place.marking + postcondition.coeff;
                    break;
                }
            }
        }
        return places;
    }
}

public class GameModel : GameElement
{
	private List<Place> initialPlaces;
	private List<Transition> initialTransitions;

    private List<Place> places;
    private List<Transition> transitions;

    private List<Place> targetPlaces;

	private void clonePlacesList(ref List<Place> clonePlaces, List<Place> places){
		Place temp;
		foreach (Place place in places) {
			temp = new Place (place.id, place.marking);
			clonePlaces.Add (temp);
		}
	}

	private void cloneTransitionsList(ref List<Transition> cloneTransitions, List<Transition> transitions){
		Transition temp;
		foreach (Transition transition in transitions) {
			temp = new Transition (transition.id, transition.preconditions, transition.postconditions); 
			cloneTransitions.Add (temp);
		}
	}

	public void initialisation(List<Place> places, List<Transition> transitions, List<Place> targetPlaces)
    {
		this.places = new List<Place> ();
		clonePlacesList (ref this.places, places);

		this.initialPlaces = new List<Place> ();
		clonePlacesList (ref this.initialPlaces, places);

		this.transitions = new List<Transition>();
		cloneTransitionsList (ref this.transitions,transitions);

		this.initialTransitions = new List<Transition>();
		cloneTransitionsList (ref this.initialTransitions,transitions);

		if (targetPlaces != null) {
			this.targetPlaces = new List<Place> ();
			clonePlacesList (ref this.targetPlaces, targetPlaces);
		}
    }

    public List<Place> performFire(int id)
    {
        foreach (Transition transition in transitions)
        {
            if (transition.id == id)
            {
                places = transition.fire(places);
                break;
            }
        }
        return places;
    }

    public bool canPerformFire(int id)
    {
        foreach (Transition transition in transitions)
        {
            if (transition.id == id)
            {
                return transition.isEnabled(places);
            }
        }
        return false;
    }

	public bool canPerformAddArc(int idTransition, int idPlace){
		bool transitionExists = false;
		bool placeExists = false;
		foreach (Transition transition in transitions) {
			if (transition.id == idTransition) {
				transitionExists = true;
			}
		}
		foreach(Place place in places){
			if (place.id == idPlace) {
				placeExists = true;
			}
		}
		return transitionExists && placeExists;
	}

	public List<Transition> performAddArc(int idTransition, int idPlace, bool direction){
        int coeff = 1;
		foreach (Transition transition in transitions) {
			if (transition.id == idTransition) {
				if(direction) {
                    for (int i = 0; i < transition.preconditions.Count; i++) // If arc exists already, remove
                    {
                        if (transition.preconditions[i].idPlace == idPlace)
                        {
                            transition.preconditions.RemoveAt(i);
                        }
                    }
                    transition.preconditions.Add (new Arc (idPlace, coeff));
				} else {
                    for (int i = 0; i < transition.postconditions.Count; i++) // If arc exists already, remove
                    {
                        if (transition.postconditions[i].idPlace == idPlace)
                        {
                            transition.postconditions.RemoveAt(i);
                        }
                    }
                    transition.postconditions.Add (new Arc (idPlace, coeff));
				}
			}
		}
        return transitions;
	}

    public List<Place> getPlaces()
    {
        return this.places;
	}

	public bool targetPetriNetReached(){
		foreach(Place targetPlace in  targetPlaces){
			foreach(Place place in places){
				if(targetPlace.id == place.id){
					if (targetPlace.marking != place.marking) {
						return false;
					}
					break;
				}
			}
		}
		return true;
	}

	public List<Place> resetPlaces(){
		places.Clear ();
		clonePlacesList (ref places, initialPlaces);
		return places;
	}

	public List<Transition> resetTransitions(){
		transitions.Clear ();
		cloneTransitionsList (ref transitions, initialTransitions);
		return transitions;
	}


	/*void Start(){
		places = new List<Place> ();
		transitions = new List<Transition> ();
		places.Add (new Place(1,1));
		places.Add (new Place(2,0));
		transitions.Add (new Transition (1, new List<Arc>(), new List<Arc>()));
		performAddArc (1, 1, 1, true);
		performAddArc (1, 2, 1, false);
		targetPlaces = new List<Place> ();
		targetPlaces.Add (new Place(1,0));
		targetPlaces.Add (new Place(2,1));
		Debug.Log (targetPetriNetReached());

		if (canPerformFire (1)) {
			performFire (1);
		}
		Debug.Log (targetPetriNetReached());
	}*/
}