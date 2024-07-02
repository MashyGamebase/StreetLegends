using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Cutscene Sequence", menuName = "Cutscenes/Dialog")]
public class CutsceneDialog : ScriptableObject
{
    [TextArea(5, 10)]
    public List<string> Dialog;

    public List<string> speaker;

    public List<bool> showSpeaker;

    public List<Image> speakerImages;
}
