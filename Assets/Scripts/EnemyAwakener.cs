using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwakener : MonoBehaviour {

    public GameObject[] enemies;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().isAwake = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().isAwake = true;
            }
        }
    }
}
