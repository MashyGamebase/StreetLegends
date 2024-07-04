using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    public CutsceneDialog dialogData;

    public TextMeshProUGUI dialogText, speakerText;

    public Image characterImageHolder;

    public int currentSequence = 0;

    public UnityEvent onShowSpeaker, onHideSpeaker, onEndDialog;

    private void Start()
    {
        StartDialog();
    }

    public void StartDialog()
    {
        DisplayNextDialog();
    }

    public void DisplayNextDialog()
    {
        onHideSpeaker.Invoke();

        if(currentSequence >= dialogData.Dialog.Count)
        {
            EndDialog();
            return;
        }

        speakerText.text = dialogData.speaker[currentSequence];
        dialogText.text = dialogData.Dialog[currentSequence];
        if (dialogData.showSpeaker[currentSequence])
        {
            onShowSpeaker.Invoke();
            if (dialogData.speakerImages[currentSequence] != null)
            {
                characterImageHolder.sprite = dialogData.speakerImages[currentSequence];
            }
        }

        currentSequence++;
    }

    private void EndDialog()
    {
        onEndDialog.Invoke();
        Debug.Log("End of Dialog Sequence");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextDialog();
        }
    }
}
