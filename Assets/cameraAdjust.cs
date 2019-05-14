using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAdjust : MonoBehaviour
{
    private float yOffset;
    private Vector3 posVector;

    
    void Start()
    {
        yOffset = 3.5f - 0.7f;
        posVector = transform.position;
    }

    internal void LateUpdate()
    {
        if (movingCube.lastCube)
        {
            if (movingCube.lastCube.name == "start")
                return;
            posVector.y = movingCube.lastCube.transform.position.y + yOffset;

            if (Vector3.Distance(transform.position, posVector) > 0.1f)
                transform.position = Vector3.Lerp(transform.position, posVector, Time.deltaTime * 2.2f);
        }
    }
}
