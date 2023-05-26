using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // Gradient & Colors
    public Gradient validGradient {get; private set;}
    public Gradient invalidGradient {get; private set;}
    
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

    /// <summary>
    /// Change the color of the Material in the MeshRenderer of a GameObject and for his childs
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="col"></param>
    public static void changeColorForObjectAndChild(GameObject obj, Color col) {
        obj.GetComponent<MeshRenderer>().material.color = col;
        foreach(Transform child in obj.transform) {
            child.GetComponent<MeshRenderer>().material.color = col;
        }
    }

    /// <summary>
    /// Change the color of the Shader in the MeshRenderer of a GameObject and for his childs
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="col"></param>
    public static void changeShaderColorForObjectAndChild(GameObject obj, Color col) {
        obj.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
        foreach(Transform child in obj.transform) {
            child.GetComponent<MeshRenderer>().material.SetColor("_Color", col);
        }
    }

    /// <summary>
    /// Change la couleur temporairement pour un effet de blink sur l'objet
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="col"></param>
    public void changeTemporaryColorForObjectAndChild(GameObject obj) {
        StartCoroutine(BlinkObjectColor(obj));
    }

    IEnumerator BlinkObjectColor(GameObject obj)
    {
        Material oldCol = obj.GetComponent<MeshRenderer>().material;
        List<Material> oldChildCol = new List<Material>();

        if(obj == null)
            yield return null;

        obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("WhiteMat");
        foreach(Transform child in obj.transform) {
            oldChildCol.Add(child.GetComponent<MeshRenderer>().material);
            child.GetComponent<MeshRenderer>().material = Resources.Load<Material>("WhiteMat");
        }

        yield return new WaitForSeconds(0.06f);

        if(obj == null)
            yield return null;

        obj.GetComponent<MeshRenderer>().material = oldCol;
        int index = 0;
        foreach(Transform child in obj.transform) {
            child.GetComponent<MeshRenderer>().material = oldChildCol[index];
            index++;
        }
    }
}