using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject attackCollider;
    public TransformVariable playerTransform;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        // The step size is equal to speed times frame time.
        float step = 5 * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.Value.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I'm triggered");
        if(other.name == "PlayerAttackCollider")
        {
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        attackCollider.SetActive(true);
    }
}
