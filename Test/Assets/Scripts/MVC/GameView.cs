using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : GameElement {
	PlaceElement[] places;
	TransitionElement[] transitions;

    public GameObject winningScreenExplosion;
    public GameObject winningPanel;

    void Start(){
		places = new PlaceElement[this.GetComponentsInChildren<PlaceElement>().Length];
		transitions = new TransitionElement[this.GetComponentsInChildren<TransitionElement>().Length];

		foreach (PlaceElement p in this.GetComponentsInChildren<PlaceElement>())
		{
			places[p.id] = p;
		}

		foreach (TransitionElement t in this.GetComponentsInChildren<TransitionElement>())
		{
			transitions[t.id] = t;
		}
	}

    // Effectuate the transition.
	public void updatePlaces(List<Place> newPlaces)
    {
		foreach (Place newPlace in newPlaces) {
			places [newPlace.id].changeMarking (newPlace.marking);
		}
    }

	public void transitionAnimation(int transitionId){
		transitions [transitionId].FireTransition ();
	}

    // Add the new arc.
    public void updateTransitions(List<Transition> newTransitions)
    {
        // Destroy all textLabels before recreating them
        GameObject[] others = GameObject.FindGameObjectsWithTag("coefficientText");
        foreach (GameObject other in others)
        { Destroy(other); }


        foreach (Transition newTransition in newTransitions)
        {
            transitions[newTransition.id].changeConditions(newTransition.preconditions, newTransition.postconditions, places);
            updateTransitionStateColor(newTransition.id, game.model.canPerformFire(newTransition.id)); // Not the best way to do it...
        }
    }

    // Return all places of the scene.
    public List<Place> getPlaces()
    {
        List<Place> places = new List<Place>();
        foreach (PlaceElement p in this.GetComponentsInChildren<PlaceElement>())
        {
			places.Add(new Place(p.id, p.initialMarking));
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

    // Display winning screen + fireworks animations
	public IEnumerator winningScreen(){
        Vector3 position = GameObject.Find("Main Camera").transform.position;
        var cam = Camera.main;
        var p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

        for (float f = p1.x; f < -p1.x; f = f - p1.x/2)
        {
            GameObject fireworks = Instantiate(winningScreenExplosion, transform);
            fireworks.transform.position = position;
            fireworks.transform.position += f * Vector3.right - p1.y * Vector3.down+ Vector3.forward * 5F;
        }

		GameObject holder = GameObject.Find ("Canvas");
		Button[] buttons = holder.GetComponentsInChildren<Button> ();
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = false;
		}
		yield return new WaitForSecondsRealtime(3);

        winningPanel.SetActive(true);
    }

	public void enableButtons(){
		GameObject holder = GameObject.Find ("Canvas");
		Button[] buttons = holder.GetComponentsInChildren<Button> ();
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = true;
		}
	}

    // Change transition color according to its state
    public void updateTransitionStateColor(int transitionId, bool newState)
    {
        transitions[transitionId].changeStatusColor(newState);
    }


}
