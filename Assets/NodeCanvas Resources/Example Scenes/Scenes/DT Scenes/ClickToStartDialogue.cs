using UnityEngine;
using System.Collections;
using NodeCanvas.DialogueTrees;

public class ClickToStartDialogue : MonoBehaviour {

	public DialogueTree dialogue;

	void OnMouseDown(){
		gameObject.SetActive(false);
		dialogue.StartDialogue( OnDialogueEnd );
	}

	void OnDialogueEnd(bool success){
		gameObject.SetActive(true);
	}
}