using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody body;


    private float horizontal;
    private float vertical;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Debug.Log(horizontal);
        Debug.Log(vertical);
        Move(horizontal, vertical);
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ouch");
    }

    private void Move(float h, float v)
    {
        body.velocity = new Vector3(h, 0.0f, v).normalized;
        
        //body.velocity = new Vector2(moveSpeed * Mathf.Sign(h), body.velocity.y);
        //transform.position += moveNormal * Time.deltaTime * MoveRate.Value;
    }
}
