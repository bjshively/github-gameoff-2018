using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayerZ : MonoBehaviour {

    public TransformVariable playerTransform;
    public EnemyAwakener awakener;
    Vector3 arenaCenter;

    bool starting = true;
    bool exiting = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move to the center of the arena while "starting"
        if (starting)
        {
            Move(arenaCenter);
        }
        if (Mathf.Abs(Vector3.Distance(transform.position, arenaCenter)) <= .1f)
        {
            starting = false;
        }

        // While exiting, move towards the player
        if (exiting)
        {
            Move(new Vector3(playerTransform.Value.position.x, 4, playerTransform.Value.position.z));

            // When close to the player, destroy the arena
            if (Mathf.Abs(Vector3.Distance(transform.position, playerTransform.Value.position)) <= .1f)
            {
                awakener.DisableRightWall();
                StartCoroutine(awakener.DestroyArena());
            }
        }
        else
        {
            // Between starting and exiting, match player Z
            transform.position = new Vector3(transform.position.x, 4, playerTransform.Value.position.z);
        }

    }

    public void Move(Vector3 pos)
    {
            transform.position = Vector3.MoveTowards(transform.position, pos, .15f);
    }

    public void SetCenter(Vector3 pos)
    {
        arenaCenter = pos;
    }

    public void BeginExitRoutine()
    {
        exiting = true;
    }
}
