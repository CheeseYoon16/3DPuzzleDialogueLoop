using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonPrompt : MonoBehaviour
{
    static InteractButtonPrompt instance;

    public static InteractButtonPrompt Instance => instance;

    [SerializeField] GameObject buttonPromptGameObject = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(instance);
    }

    public void ShowPrompt(bool interactable)
    {
        buttonPromptGameObject.SetActive(interactable);
    }


}
