using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScroller : MonoBehaviour {

    public float scrollSpeed = 3;
    Vector3 end; 
	// Use this for initialization
	void Start () {
        end = new Vector3(transform.position.x, 2550, transform.position.z);
        StartCoroutine(RestartGame());
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, end, scrollSpeed);
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene("StartMenu");
    }
}
