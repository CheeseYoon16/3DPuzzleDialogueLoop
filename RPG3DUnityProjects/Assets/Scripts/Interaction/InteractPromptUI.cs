using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractPromptUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] Image promptImage;
    [SerializeField] GameObject promptGameobject;

    [SerializeField] float promptOffset_Y = 1f;

    private void OnEnable()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void SetPromptEnabled(bool enabled, Vector3 position, InteractionPromptData prompt = null)
    {
        if (promptGameobject == null)
        {
            Debug.LogError($"The prompt gameobject is null, make sure you've assigned it first");
            return;
        }

        if(prompt == null)
        {
            Debug.LogWarning($"Interacting with interactable that have no prompt data attached");
            promptGameobject.SetActive(false);
            return;

        }

        promptGameobject.SetActive(enabled);

        if(enabled)
        {
            SetPrompt(prompt, position);
        }
        else
        {
            promptText.text = string.Empty;
        }
    }

    private void SetPrompt(InteractionPromptData prompt, Vector3 position)
    {
        if(promptText != null && promptImage!=null && prompt != null)
        {
            promptText.text = prompt.promptText;

            if(prompt.promptImage != null)
            {
                promptImage.sprite = prompt.promptImage;
            }

            transform.position = new Vector3(position.x, position.y + promptOffset_Y, position.z);
        }
    }

}
