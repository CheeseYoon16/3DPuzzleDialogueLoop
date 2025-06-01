using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSequenceData", menuName = "EliEntertainment/DialogueSeqData")]
public class DialogueSeqData : ScriptableObject
{
    public List<DialogueData> listOfDialogue = new List<DialogueData>();
}
