using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public movingCube blockPrefab;
    public MoveDir dir;

    public void spawnCube()
    {
        movingCube block = Instantiate(blockPrefab);
        block.moveDir = dir;

        float x_offset = dir == spawner.MoveDir.X ? transform.position.x : movingCube.lastCube.transform.position.x;
        float z_offset = dir == spawner.MoveDir.Z ? transform.position.z : movingCube.lastCube.transform.position.z;

        if (movingCube.lastCube.name == "start")
        {
            block.transform.position = transform.position;
        }
        else
        {
            block.transform.position = new Vector3(x_offset,
                movingCube.lastCube.transform.position.y + blockPrefab.transform.localScale.y, z_offset);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, blockPrefab.transform.localScale);
    }

    public enum MoveDir
    {
       X = 0,Z = 1
    };
}
