using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwakener : MonoBehaviour {

    public GameObject[] enemies;

	// Use this for initialization
	void Start () {
        // Start all enemies in a sleep state
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().SetAwake(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        // When the player passes through the trigger, awaken all enemies
        if(other.name == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().SetAwake(true);
            }
        }
    }
}
