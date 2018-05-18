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

        var pixelater = new PixelateTransition()
        {
            nextScene = nextSceneID,
            finalScaleEffect = PixelateTransition.PixelateFinalScaleEffect.ToPoint,
            duration = 1.0f
        };
        if (Random.Range(0, 1) < 0.5F)
        {
            TransitionKit.instance.transitionWithDelegate(pixelater);
        } else
        {

            TransitionKit.instance.transitionWithDelegate(fader);
        }
    }
}
