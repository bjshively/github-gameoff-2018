using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float lifetime = 6f;
    public float moveSpeed = .5f;
    Vector3 target;
    Vector3 moveDir;
    public TransformVariable playerTransform;

	// Use this for initialization
	void Start () {
        moveDir = (playerTransform.Value.position - transform.position).normalized;
        target = new Vector3(playerTransform.Value.position.x +20, playerTransform.Value.position.y, playerTransform.Value.position.z + 20);
	}
	
	void Update () {
        lifetime -= Time.deltaTime;
        transform.position += moveDir * moveSpeed;

        // Bullets die once they reach their target or their lifetime has elapsed
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
