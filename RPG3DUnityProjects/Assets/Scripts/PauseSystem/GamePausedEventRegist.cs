using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePausedEventRegist : MonoBehaviour
{
    public UnityEvent onGamePaused = new UnityEvent();
    public UnityEvent onGamePausedStop = new UnityEvent();
    bool haveInit = false;

    public bool HaveInit => haveInit;

    IEnumerator RegisterActionCorout()
    {
        yield return new WaitUntil(()=> GamePausedListener.Instance != null);


        if (!haveInit)
        {
            if (GamePausedListener.Instance != null)
            {
                GamePausedListener.Instance.AddListenerOnPause(() =>
                {
                    onGamePaused.Invoke();
                });

                GamePausedListener.Instance.AddListenerOnPauseStop(() =>
                {
                    onGamePausedStop.Invoke();
                });

                haveInit = true;
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(RegisterActionCorout());
    }
}
