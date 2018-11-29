using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        // When the player passes through the trigger, awaken all enemies
        if (other.name == "Player")
        {
            transform.parent.GetComponent<Car>().SetAwake(true);
        }
    }
}
