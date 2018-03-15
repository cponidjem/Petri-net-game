using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class SceneTransition : MonoBehaviour {

    public int nextSceneID;

    public void ChangeScene()
    {
        FadeTransition fader = new FadeTransition()
        {
            nextScene = nextSceneID,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }
}
