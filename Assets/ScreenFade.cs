using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour {
    Material black;
    Color color;
    public Text youDied;
    public IntVariable playerHealth;
    bool activated = false;

    // Use this for initialization
    void Start()
    {
        black = GetComponent<RawImage>().material;
        GetComponent<RawImage>().material = null;

    }
    // Update is called once per frame
    void Update()
    {
        if (playerHealth.Value <= 0 && !activated)
        {
            activated = true;
            StartCoroutine(BeginRestart());
        }
    }

    // Turn screen black and show you died text
    public void TurnScreenBlack()
    {
        GetComponent<RawImage>().material = black;
        youDied.gameObject.SetActive(true);
        StartCoroutine(RestartScene());
    }

    // Restart scene after 3 seconds 
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Need this to delay slightly between player death and restart beginning
    IEnumerator BeginRestart()
    {
        yield return new WaitForSeconds(2);
        TurnScreenBlack();
    }



}