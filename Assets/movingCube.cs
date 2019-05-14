using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class movingCube : MonoBehaviour
{
    public static movingCube currentCube { get; internal set; }
    public static movingCube lastCube { get; internal set; }

    public spawner.MoveDir moveDir;

    public float moveSpeed = 1f;

    private bool moveBack = false;

    private void OnEnable()
    {
        // cubu de inceput devine last cube
        if (lastCube == null)
        {    
            lastCube = GameObject.Find("start").GetComponent<movingCube>();
        }
        
        currentCube = this;

        transform.localScale = new Vector3(lastCube.transform.localScale.x, transform.localScale.y, lastCube.transform.localScale.z);
    }

    internal void placeCube()
    {
        float offset;
        moveSpeed = 0f;


        if (gameObject.name == "start")
        {
            GameObject.Find("Canvas").SetActive(false);
            return;
        } 

        // daca nu se ating deloc
        if (moveDir == spawner.MoveDir.Z)
        {
            offset = transform.position.z - lastCube.transform.position.z;
            if (Mathf.Abs(offset) >= lastCube.transform.localScale.z)
            {
                gameObject.AddComponent<Rigidbody>();
                return;
            }
        }
        else
        {
            offset = transform.position.x - lastCube.transform.position.x;
            if (Mathf.Abs(offset) >= lastCube.transform.localScale.x)
            {
                gameObject.AddComponent<Rigidbody>();
                return;
            }
        }


        // x sau z
        float direction = (offset > 0f) ? 1f : -1f;

        // daca sunt foarte aproape sa le puna ok ( marja de eroare)
        if (Mathf.Abs(offset) < 0.08f)
        {
            transform.position = new Vector3(lastCube.transform.position.x, transform.position.y, lastCube.transform.position.z);
        }
        else // daca nu, le taie
        {
            if (moveDir == spawner.MoveDir.Z)
            {
                splitCubeOnZ(offset, direction);
            }
            else
            {
                splitCubeOnX(offset, direction);
            }
        }

        lastCube = this;
    }

    private void splitCubeOnZ(float offset,float direction)
    {
        float newSize = lastCube.transform.localScale.z - Mathf.Abs(offset);
        float deadCubeSize = transform.localScale.z - newSize;

       
        float newPosZ = lastCube.transform.position.z + (offset / 2);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosZ);

        float edgePos = transform.position.z + (transform.localScale.z / 2) * direction;
        float deadCubePosition = edgePos + (deadCubeSize / 2) * direction;

        spawnDeadCube(deadCubePosition, deadCubeSize);
    }

    private void splitCubeOnX(float offset, float direction)
    {
        float newSize = lastCube.transform.localScale.x - Mathf.Abs(offset);
        float deadCubeSize = transform.localScale.x - newSize;


        float newPosZ = lastCube.transform.position.x + (offset / 2);

        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newPosZ, transform.position.y, transform.position.z);

        float edgePos = transform.position.x + (transform.localScale.x / 2) * direction;
        float deadCubePosition = edgePos + (deadCubeSize / 2) * direction;

        spawnDeadCube(deadCubePosition, deadCubeSize);
    }

    // spawn cubu care cade
    internal void spawnDeadCube(float deadCubePosition, float deadCubeSize)
    {
        GameObject deadCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (moveDir == spawner.MoveDir.Z)
        {
            deadCube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, deadCubeSize);
            deadCube.transform.position = new Vector3(transform.position.x, transform.position.y, deadCubePosition);
        }
        else
        {
            deadCube.transform.localScale = new Vector3(deadCubeSize, transform.localScale.y, transform.localScale.z);
            deadCube.transform.position = new Vector3(deadCubePosition, transform.position.y, transform.position.z);
        }

        deadCube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Rigidbody tempRb = deadCube.AddComponent<Rigidbody>();
        tempRb.mass = 2f;
    }


    // miscarea inainte inapoi
    void Update()
    {
        
        if (transform.position.z < -2 || transform.position.x < -2)
        {
            moveBack = false;
        }
        else if (transform.position.z > 2 || transform.position.x > 2)
        {
            moveBack = true;
        }

        if (moveDir == spawner.MoveDir.Z)
        {
            if (!moveBack)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (!moveBack)
            {
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position -= transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }
}
