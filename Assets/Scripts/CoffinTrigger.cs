using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinTrigger : MonoBehaviour {

    public GameObject coffin;

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
            coffin.SetActive(true);
            coffin.GetComponent<Coffin>().SetAwake(true);
        }
    }
}
