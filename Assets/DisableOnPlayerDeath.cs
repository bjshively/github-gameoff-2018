using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnPlayerDeath : MonoBehaviour {
    public IntVariable playerHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(playerHealth.Value <= 0)
        {
            gameObject.SetActive(false);
        }
	}
}
