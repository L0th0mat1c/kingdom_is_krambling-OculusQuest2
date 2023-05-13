using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // Gradient & Colors
    public Gradient validGradient;
    public Gradient invalidGradient;
    
    //Parameters
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    //Instance Singleton
    private static ColorManager instance = null;
    public static ColorManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    void Start() {
        //Valid gradient
        validGradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.cyan;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.cyan;
        colorKey[1].time = 1.0f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;
        validGradient.SetKeys(colorKey, alphaKey);

        //Invalid gradient
        invalidGradient = new Gradient();
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;
        invalidGradient.SetKeys(colorKey, alphaKey);
    }
}
