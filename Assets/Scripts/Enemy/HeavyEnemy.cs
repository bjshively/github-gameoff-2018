using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy {

    public GameObject smashCollider;

	//// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    override protected void Attack()
    {
        anim.SetTrigger("Combo3");
    }
}
