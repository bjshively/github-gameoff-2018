using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour {
    public DialogueCanvasController dialogueBox;
    public string message;
    public float displayTime = 3;
    bool triggered = false;

	// Use this for initialization
	void Start () {
        dialogueBox.GetComponent<DialogueCanvasController>();
	}
	
	// Update is called once per frame
	void Update () {

	}


    public void ShowMessage(string text) {
        dialogueBox.ActivateCanvasWithText(text);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && !triggered)
        {
            triggered = true;
            ShowMessage(message);
            StartCoroutine(HideDialogue());
        }
    }

    IEnumerator HideDialogue()
    {
        yield return new WaitForSeconds(displayTime);
        dialogueBox.gameObject.SetActive(false);
    }

}
