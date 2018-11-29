using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour {
    Material black;
    Color color;
    public Text youDied;

    // Use this for initialization
    void Start()
    {
        black = GetComponent<RawImage>().material;
        GetComponent<RawImage>().material = null;

    }
	// Update is called once per frame
	void Update () {
    }


    public void TurnScreenBlack()
    {
        GetComponent<RawImage>().material = black;
        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        youDied.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}