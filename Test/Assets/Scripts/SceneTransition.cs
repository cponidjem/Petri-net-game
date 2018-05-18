using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class SceneTransition : MonoBehaviour {

    public Shader chosenShader;

    public void ChangeScene(int nextSceneID)
    {

        var pixelater = new PixelateTransition()
        {
            nextScene = nextSceneID,
            finalScaleEffect = PixelateTransition.PixelateFinalScaleEffect.ToPoint,
            duration = 1.0f,
            usedShader = chosenShader
        };
        TransitionKit.instance.transitionWithDelegate(pixelater);
    }
}
