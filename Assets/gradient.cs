using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class gradient : MonoBehaviour
{
    public static gradient Instance { get; set; }

    internal Color currentColor;
    internal Color currentColorTarget;

    public int swapCounter = 0;


    private void Awake()
    {
        Instance = this;
        currentColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        currentColorTarget = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    internal Color lerpColor()
    {
        if (swapCounter == 11)
        {
            currentColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            currentColorTarget = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            swapCounter = 0;
        }
        else
        {
            currentColor = Color.Lerp(currentColor, currentColorTarget, 0.1f);
            ++swapCounter;
        }

        return currentColor;
    }

}
