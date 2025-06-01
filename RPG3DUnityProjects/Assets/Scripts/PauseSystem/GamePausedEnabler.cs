using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePausedEnabler : MonoBehaviour
{
    private void OnEnable()
    {
        if (GamePausedListener.Instance != null)
        {
            GamePausedListener.Instance.InvokePause();
        }
    }

    private void OnDisable()
    {
        if (GamePausedListener.Instance != null)
        {
            GamePausedListener.Instance.InvokeOnPauseStop();
        }
    }
}
