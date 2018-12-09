using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public float minimumSecondsUntilNextFlicker = 0;
    public float maximumSecondsUntilNextFlicker = 3;
    public float minimumIntensity = 0;
    public float maximumIntensity = 2;
    public float flickerMinimumLength = .1f;
    public float flickerMaximumLength = .5f;
    float startingIntensity;
    Light lamp;

    float timeToWait;
    float currentFlickerTime;

    // Use this for initialization
    void Start()
    {
        lamp = GetComponent<Light>();
        startingIntensity = lamp.intensity;
        SetFlickerLength();
    }

    // Update is called once per frame
    void Update()
    {
        Flicker();
    }

    // Set a random intensity for the light between min and max values
    void Flicker()
    {
        // Flicker if there is flicker time
        if (currentFlickerTime >= 0)
        {
            lamp.intensity = Random.Range(minimumIntensity, maximumIntensity);
            currentFlickerTime -= Time.deltaTime;
        }

        // Else, wait if there is wait time
        else
        {
            ResetLight();
            if (timeToWait >= 0)
            {
                timeToWait -= Time.deltaTime;
            }
            else
            {
                // Else, reset and resume flickering
                SetFlickerWait();
                SetFlickerLength();
            }
        }


    }

    void SetFlickerLength()
    {
        currentFlickerTime = Random.Range(flickerMinimumLength, flickerMaximumLength);
    }

    // Set the number of seconds until the next flicker
    void SetFlickerWait()
    {
        timeToWait = Random.Range(minimumSecondsUntilNextFlicker, maximumSecondsUntilNextFlicker);
    }

    void ResetLight()
    {
        lamp.intensity = startingIntensity;
    }
}
