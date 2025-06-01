using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "EliEntertainment/DialogueData")] 
public class DialogueData : ScriptableObject
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

}
