using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : GameElement {

    public GameObject memoryPrefab;

    void Start()
    {
        // Initialize level ending using another scene
        string sceneName = SceneManager.GetActiveScene().name;
        MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
        if (!sceneName.EndsWith("_end") && memory == null){
            string endSceneName = sceneName + "_end";
            SceneManager.LoadSceneAsync(endSceneName);
            memory = Instantiate<MemoryScript>(memoryPrefab.GetComponent<MemoryScript>());
            SceneManager.LoadSceneAsync(sceneName);
        }
        else if (sceneName.EndsWith("_end")) {
            Debug.Log("Scene used only for setting the end.");
            memory.setEndPlaces(game.view.getPlaces());
        }
        else{
            Debug.Log("Unnecessary replica");
        }


        // Initialise game model using the scene
        game.model.initialisation(game.view.getPlaces(), game.view.getTransitions());


    }

    public void OnTransitionClicked(int transitionId)
    {
		List<Place> places = new List<Place> ();
        // Check if preconditions are filled.
        if (game.model.canPerformFire(transitionId))
        {
            // Effectuate the transition on the model
            places = game.model.performFire(transitionId);
            Debug.Log("Transition fired.");

			//Animation
			game.view.transitionAnimation (transitionId);

            // Update graphics
			game.view.updatePlaces(places);

            
        } else
        {
            Debug.Log("Transition cannot be fired.");
        }

    }

    public void OnArcWasDrawn(int transitionId, int placeId, bool direction)
    {
        List<Transition> transitions;
        if (game.model.canPerformAddArc(transitionId,placeId))
        {
            transitions = game.model.performAddArc(transitionId, placeId, direction);
            game.view.updateTransitions(transitions);

        }
        else
        {
            Debug.Log("Transition or place does not exist.");
        }
    }

}
