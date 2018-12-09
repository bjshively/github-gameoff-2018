using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankLoader : MonoBehaviour {

    public AK.Wwise.Bank GameSoundbank;

	void Awake ()
    {
        GameSoundbank.Load();
	}
	

}
