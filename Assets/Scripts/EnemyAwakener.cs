using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyAwakener : MonoBehaviour {

    public TransformVariable playerTransform;
    public List<GameObject> enemies;
    public bool isArena;
    public GameObject[] walls;
    public Transform focalPoint;
    CinemachineVirtualCamera cam;
    Transform playerCamFollow;
    Vector3 middleOfArena;

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
                focalPoint.GetComponent<MatchPlayerZ>().BeginExitRoutine();
                StartCoroutine(DestroyArena());
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
                middleOfArena = new Vector3((walls[0].transform.position.x + walls[1].transform.position.x) / 2, 4, playerTransform.Value.position.z);
                focalPoint.gameObject.SetActive(true);
                focalPoint.gameObject.transform.position = new Vector3 (playerTransform.Value.position.x, 4, playerTransform.Value.position.z);
                cam.Follow = focalPoint;
                cam.LookAt = focalPoint;
                focalPoint.GetComponent<MatchPlayerZ>().SetCenter(middleOfArena);
            }
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void ToggleWalls(bool b)
    {
        for (int i = 0; i < 2; i++)
        {
            walls[i].SetActive(b);
        }
    }

    public void DisableRightWall()
    {
        walls[1].SetActive(false);
    }

    public IEnumerator DestroyArena()
    {
       
        yield return new WaitForSeconds(3);
        // If all the enemies are dead, turn off the second wall and trigger, reset camera follow
        Destroy(walls[1].gameObject);
        cam.Follow = playerCamFollow;
        cam.LookAt = playerCamFollow;
        gameObject.SetActive(false);
    }
}
