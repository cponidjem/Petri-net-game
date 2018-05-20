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
		if (!sceneName.EndsWith("_end") && !memory.getLoaded()){
            // Scene used to load end scene and then load back the real scene
            string endSceneName = sceneName + "_end";
			memory.setLoaded (true);
			SceneManager.LoadSceneAsync(endSceneName);
			SceneManager.LoadSceneAsync(sceneName);
        }
        else if (sceneName.EndsWith("_end")) {
            //Scene used only for setting the end
            memory.setEndPlaces(game.view.getPlaces());
        }
        else{
            //Scene that we use to play
			memory.setLoaded(false);
        }


        // Initialise game model using the scene
		game.model.initialisation(game.view.getPlaces(), game.view.getTransitions(),memory.getEndPlaces());
        game.view.updateTransitions(game.view.getTransitions());

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
            game.view.updateTransitions(game.view.getTransitions());

            // If end is reached, memorize last level completed
            if (game.model.targetPetriNetReached ()) {
				game.view.winningScreen ();
				Debug.Log ("End reached.");
                MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
                int lastLevelCompleted = int.Parse(SceneManager.GetActiveScene().name.Substring("Level_".Length));
				if (lastLevelCompleted > memory.getLastLevelCompleted()) {
					memory.setLastLevelCompleted(lastLevelCompleted);
				}
                
            }

            
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

	public void OnResetClicked(){
		/*string sceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadSceneAsync(sceneName);*/
		List<Place> newPlaces = game.model.resetPlaces ();
		List<Transition> newTransitions = game.model.resetTransitions ();
		game.view.updatePlaces (newPlaces);
		game.view.updateTransitions (newTransitions);
		Debug.Log ("reset");
	}

}
