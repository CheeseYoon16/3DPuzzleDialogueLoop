using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "interactionPromptData", menuName = "EliEntertainment/Interaction/promptData")]
public class InteractionPromptData : ScriptableObject
{
    public string ID => this.name;

    public string promptText = string.Empty;

    public Sprite promptImage = null;
}
