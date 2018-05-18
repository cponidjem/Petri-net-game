using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public void SetVolume()
    {
        GameObject.FindObjectOfType<AudioSource>().volume = GetComponent<Slider>().value;
    }
}
