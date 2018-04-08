using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GameElement {

    void Start()
    {
        // Initialise game model using the scene
        game.model.initialisation(game.view.getPlaces(), game.view.getTransitions());
    }

    public void OnTransitionClicked(int transitionId)
    {
        // Check if preconditions are filled.
        if (game.model.canPerformFire(transitionId))
        {
            // Effectuate the transition on the model
            game.model.performFire(transitionId);
            Debug.Log("Transition fired.");

            // Fire the transition in graphics
            game.view.FireTransition(transitionId);
            
        } else
        {
            Debug.Log("Transition cannot be fired.");
        }
    }

}
