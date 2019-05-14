using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private spawner.MoveDir moveDir;

    protected spawner z_spawner;
    protected spawner x_spawner;
    public Canvas canvas;

    // singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        moveDir = spawner.MoveDir.Z;
        z_spawner = GameObject.Find("spawner_z").GetComponent<spawner>();
        x_spawner = GameObject.Find("spawner_x").GetComponent<spawner>();
        GameObject.Find("start").GetComponent<Renderer>().material.color = gradient.Instance.currentColor;
    }

    private void Update()
    {
        //check game over
        if (movingCube.lastCube)
        {
            if (movingCube.lastCube.transform.localScale.x <= 0.04f || movingCube.lastCube.transform.localScale.z <= 0.04f)
            {
                canvas.gameObject.SetActive(true);
                canvas.transform.GetChild(0).GetComponent<Text>().text = "game over :P";

                z_spawner.gameObject.SetActive(false);
                x_spawner.gameObject.SetActive(false);


                movingCube.currentCube = null;
                movingCube.lastCube.gameObject.SetActive(false);
                movingCube.lastCube = null;

                Invoke("loadScene", 3f);
                return;
            }
        }
        else
        { return; }

        if (Input.GetMouseButtonDown(0))
        {
            placeBlock();
        }
    }

    protected void placeBlock()
    {
        if (movingCube.currentCube != null)
            movingCube.currentCube.placeCube();

        if (moveDir == spawner.MoveDir.Z)
        {
            z_spawner.spawnCube();
            moveDir = spawner.MoveDir.X;
        }
        else
        {
            x_spawner.spawnCube();
            moveDir = spawner.MoveDir.Z;
        }

        movingCube.currentCube.GetComponent<Renderer>().material.color = gradient.Instance.lerpColor();
    }

    internal void loadScene()
    {
        SceneManager.LoadScene(0);
    }




}
