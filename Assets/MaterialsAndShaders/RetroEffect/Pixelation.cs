using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pixelation : MonoBehaviour {

    public Material EffectMaterial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRenderImage(RenderTexture Source, RenderTexture Destination)
    {
        Graphics.Blit(Source, Destination, EffectMaterial);
    }
}
