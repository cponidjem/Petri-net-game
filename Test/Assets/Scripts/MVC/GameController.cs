﻿using System.Collections;
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
			memory.setEndTransitions (game.view.getTransitions ());
        }
        else{
            //Scene that we use to play
			memory.setLoaded(false);
        }


        // Initialise game model using the scene
		game.model.initialisation(game.view.getPlaces(), game.view.getTransitions(),memory.getEndPlaces(),memory.getEndTransitions());
		/*string str = "";
		foreach (Transition t in tr) {
			str += t.id;
			str+=" preconditions: ";
			foreach(Arc pre in t.preconditions){
				str+= (pre.idPlace + ", " + pre.coeff+"; ");
			}
			str+= (" postconditions ");
			foreach(Arc post in t.postconditions){
				str+= (post.idPlace + ", " + post.coeff+"; ");
			}
		}
		Debug.Log (str);*/
		game.view.updateTransitions(game.model.getTransitions());

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
			game.view.updateTransitions(game.model.getTransitions());
			int lastLevelCompleted = int.Parse(SceneManager.GetActiveScene().name.Substring("Level_".Length));
			if (lastLevelCompleted != 2) {
				if ((lastLevelCompleted == 5 && game.model.targetPetriNetReached2()) || lastLevelCompleted!=5) {
					// If end is reached, memorize last level completed
					if (game.model.targetPetriNetReached ()) {
						StartCoroutine (game.view.winningScreen ());
						Debug.Log ("End reached.");
						MemoryScript memory = GameObject.FindObjectOfType<MemoryScript> ();
						if (lastLevelCompleted > memory.getLastLevelCompleted ()) {
							memory.setLastLevelCompleted (lastLevelCompleted);
						}

					}
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
			int lastLevelCompleted = int.Parse(SceneManager.GetActiveScene().name.Substring("Level_".Length));
			if (lastLevelCompleted == 2) {
				// If end is reached, memorize last level completed
				if (game.model.targetPetriNetReached2 ()) {
					StartCoroutine(game.view.winningScreen ());
					Debug.Log ("End reached.");
					MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();

					if (lastLevelCompleted > memory.getLastLevelCompleted()) {
						memory.setLastLevelCompleted(lastLevelCompleted);
					}

				}
			}
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
		game.view.enableButtons ();
		Debug.Log ("reset");
	}

    public void OnAddTokensClicked(int placeId)
    {
        game.model.addTokens(placeId);
        game.view.updatePlaces(game.model.getPlaces());
		// If end is reached, memorize last level completed
		if (game.model.targetPetriNetReached ()) {
			StartCoroutine(game.view.winningScreen ());
			Debug.Log ("End reached.");
			MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
			int lastLevelCompleted = int.Parse(SceneManager.GetActiveScene().name.Substring("Level_".Length));
			if (lastLevelCompleted > memory.getLastLevelCompleted()) {
				memory.setLastLevelCompleted(lastLevelCompleted);
			}

		}
    }

	void OnApplicationPause(){
		MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
		if (memory != null) {
			memory.Save ();
		}
	}

	void OnApplicationQuit(){
		MemoryScript memory = GameObject.FindObjectOfType<MemoryScript>();
		if (memory != null) {
			memory.Save ();
		}
	}



}
