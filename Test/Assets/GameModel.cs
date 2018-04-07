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

    public Transition(int id, List<Arc> preconditions, List<Arc> postconditions)
    {
        this.id = id;
        this.preconditions = preconditions;
        this.postconditions = postconditions;
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
                        Debug.Log(precondition.coeff);
                        Debug.Log(place.marking);
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
    List<Place> places;
    List<Transition> transitions;

    public void initialisation(List<Place> places, List<Transition> transitions)
    {
        this.places = places;
        this.transitions = transitions;
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
}

