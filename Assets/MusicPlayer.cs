using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public bool MusicStarted = false;
    public AK.Wwise.Event GameplayMusic;

	// Use this for initialization
	void Start ()
    {

        if (MusicStarted == false)
        {

            GameplayMusic.Post(gameObject);
            MusicStarted = true;


        }
        else
        {
            Debug.Log("Music Is Already Playing");
        }
	}
	

}
