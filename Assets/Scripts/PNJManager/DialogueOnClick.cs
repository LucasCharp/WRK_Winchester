using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogueOnClick : MonoBehaviour
{
    private Dialogues dialogues;

    private void Start()
    {
        dialogues = GameObject.FindObjectOfType<Dialogues>();
    }
    public void OnMouseDown()
    {
        Debug.Log("Je clique sur le pnj aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        dialogues.SayDialogueHumain();
        
    }
}
