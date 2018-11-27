using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public AK.Wwise.Event MenuMusic;
    public AK.Wwise.Event StartTransition;

	// Use this for initialization
	void Start () {

        MenuMusic.Post(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        StartTransition.Post(gameObject);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
