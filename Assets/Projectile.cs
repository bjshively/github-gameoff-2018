using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float lifetime = 6f;
    public float moveSpeed = .5f;
    Vector3 target;
    public TransformVariable playerTransform;

	// Use this for initialization
	void Start () {
        target = playerTransform.Value.position;
	}
	
	void Update () {
        lifetime -= Time.deltaTime;

        // Bullets die once they reach their target or their lifetime has elapsed
        if (lifetime <= 0 || Mathf.Abs(Vector3.Distance(transform.position, target)) <= 1)
        {
            Die();
        }
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
	}

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Die();
        }
    }
}
