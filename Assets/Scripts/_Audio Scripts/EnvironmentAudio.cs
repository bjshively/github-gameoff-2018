using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAudio : MonoBehaviour
{
    public GameObject SceneObject;
    public WwiseEventVariable GameplayMusic;
    public WwiseEventVariable GameplayAmbience;

    void Start()
    {
        GameplayMusic.Value.Post(SceneObject);
        GameplayAmbience.Value.Post(SceneObject);
    }
}
