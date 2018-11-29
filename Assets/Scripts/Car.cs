using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    Vector3 target;
    public float step = .5f;
    BoxCollider col;
    bool isAwake = false;

    // 1 = right, -1 = left
    public int direction = 1;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAwake)
        {
            target = new Vector3(transform.position.x + 100 * direction, transform.position.y, transform.position.z);
            Move();

            // Destroy the object once it is far away from the player
            if (Mathf.Abs(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position)) > 50)
            {
                Destroy(gameObject);
            }
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            col.enabled = false;
        }
    }

    public void SetAwake(bool state)
    {
        isAwake = state;
        //anim.SetBool("Awake", state);
    }
}
