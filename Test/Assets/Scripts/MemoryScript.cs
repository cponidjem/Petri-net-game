using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Thing where we can store information between scenes. Shoul be replaced when better solution emerges.
public class MemoryScript : MonoBehaviour
{
    private List<Place> targetPlaces;
    private int lastLevelCompleted;
	private bool loaded = false;

	public bool getLoaded() {
		return loaded;
	}

	public void setLoaded(bool state){
		loaded = state;
	}

    public List<Place> getEndPlaces()
    {
        return targetPlaces;
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
		lastLevelCompleted = 0;
        Load();
    }

    public void setEndPlaces(List<Place> places)
    {
        this.targetPlaces = places;
    }

	public int getLastLevelCompleted()
	{
		return this.lastLevelCompleted;
	}

    public void setLastLevelCompleted(int level)
    {
        this.lastLevelCompleted = level;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerProgress.dat");

        PlayerProgress progress = new PlayerProgress();
        progress.lastLevelCompleted = lastLevelCompleted;

        bf.Serialize(file, progress);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerProgress.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerProgress.dat", FileMode.Open);
            PlayerProgress progress = (PlayerProgress)bf.Deserialize(file);
            file.Close();
            lastLevelCompleted = progress.lastLevelCompleted;
        }
    }
}

[Serializable]
class PlayerProgress
{
    public int lastLevelCompleted;
}