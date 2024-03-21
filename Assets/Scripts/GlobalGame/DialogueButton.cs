using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public bool clicked = false;

    public void OnDialogueButtonClicked()
    {
        clicked = true;
    }
}
