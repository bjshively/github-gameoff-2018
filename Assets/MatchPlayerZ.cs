using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPlayerZ : MonoBehaviour {

    public TransformVariable playerTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, playerTransform.Value.position.z);
	}
}
