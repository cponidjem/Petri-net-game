using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all elements in the game
public class GameElement : MonoBehaviour
{
    // Access to the game
    public Game game { get { return GameObject.FindObjectOfType<Game>(); } }

    // Get position from the Transform 
    public Vector3 getPosition()
    {
        return transform.position;
    }
}

public class Game : MonoBehaviour {

    // Model, view and Controller
    public GameModel model;
    public GameView view;
    public GameController controller;

	// Initialize model, view and controller
	void Start () {
        this.model = GetComponentInChildren<GameModel>();
        this.view = GetComponentInChildren<GameView>();
        this.controller = GetComponentInChildren<GameController>();
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
