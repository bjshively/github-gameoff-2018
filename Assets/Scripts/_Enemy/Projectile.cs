using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float lifetime = 6f;
    public float moveSpeed = .5f;
    Vector3 moveDir;
    public TransformVariable playerTransform;

	// Use this for initialization
	void Start () {
        // Calculate movedir based on player X and Z, but bullet's Y to avoid Y axis shifting.
        moveDir = (new Vector3(playerTransform.Value.position.x, transform.position.y, playerTransform.Value.position.z) - transform.position).normalized;
	}
	
	void Update () {
        lifetime -= Time.deltaTime;
        transform.position += moveDir * moveSpeed;

        // Bullets die once their lifetime has elapsed
        if (lifetime <= 0)
        {
            Die();
        }
        
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
