using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public AK.Wwise.Event MenuMusic;
    public AK.Wwise.Event StartTransition;

	// Use this for initialization
	void Start ()
    { 
        MenuMusic.Post(gameObject);
	}
	
    public void StartGame()
    {
        StartTransition.Post(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
