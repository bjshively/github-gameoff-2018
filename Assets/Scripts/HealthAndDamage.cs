using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndDamage : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            Die();
        }
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerAttackCollider")
        {
            TakeDamage(50);
        }
    }
}
