using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Thing where we can store information between scenes. Shoul be replaced when better solution emerges.
public class MemoryScript : MonoBehaviour {

    private List<Place> targetPlaces;
    private int lastLevelCompleted;

	public List<Place> getEndPlaces(){
		return targetPlaces;
	}

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}

    public void setEndPlaces(List<Place> places)
    {
        this.targetPlaces = places;
    }

    public void setLastLevelCompleted(int level)
    {
        this.lastLevelCompleted = level;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
