using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public float minimumSecondsUntilNextFlicker = 0;
    public float maximumSecondsUntilNextFlicker = 3;
    public float minimumIntensity = 0;
    public float maximumIntensity = 2;
    public float flickerSpeed = 1;
    Light lamp;
    float time;
    float timeOfNextFlicker;

	// Use this for initialization
	void Start () {
        SetFlickerTime();
        lamp = transform.Find("Point Light").GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - time >= timeOfNextFlicker)
        {
            SetFlickerTime();
            Flicker();
        }
    }

    void Flicker()
    {
            lamp.intensity = Random.Range(minimumIntensity, maximumIntensity);
    }

    void SetFlickerTime()
    {
        time = Time.time;
        timeOfNextFlicker = Random.Range(minimumSecondsUntilNextFlicker, maximumSecondsUntilNextFlicker);
    }
}
