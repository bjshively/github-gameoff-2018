using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyAwakener : MonoBehaviour {

    public List<GameObject> enemies;
    public bool isArena;
    public GameObject[] walls;
    CinemachineVirtualCamera cam;
    Transform playerCamFollow;
   

	// Use this for initialization
	void Start () {
        playerCamFollow = GameObject.Find("Player").transform.Find("CameraFollow").transform;
        // Start all enemies in a sleep state
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Enemy>().SetAwake(false);
        }
        cam = GameObject.Find("Cinemachine").GetComponent<CinemachineVirtualCamera>();
    }
	
	// Update is called once per frame
	void Update () {
		for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if(enemies[i].GetComponent<Enemy>().health <= 0)
            {
                enemies.Remove(enemies[i]);
            }
        }
        if (isArena)
        {
            if (enemies.Count == 0)
            {
                // If all the enemies are dead, turn off the second wall and trigger, reset camera follow
                walls[1].SetActive(false);
                cam.Follow = playerCamFollow;
                cam.LookAt = playerCamFollow;
                gameObject.SetActive(false);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        // When the player passes through the trigger, awaken all enemies
        if(other.name == "Player")
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].GetComponent<Enemy>().SetAwake(true);
            }

            if(isArena)
            {
                ToggleWalls(true);
                cam.Follow = transform;
                cam.LookAt = transform;
            }
        }
    }

    void ToggleWalls(bool b)
    {
        for (int i = 0; i < 2; i++)
        {
            walls[i].SetActive(b);
        }
    }
}
