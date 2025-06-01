using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePausedListener : MonoBehaviour
{
    static GamePausedListener instance;

    public static GamePausedListener Instance => instance;
    List<UnityAction> actionsOnPaused = new List<UnityAction>();
    List<UnityAction> actionsOnPauseStop = new List<UnityAction>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(instance);
    }

    public void AddListenerOnPause(UnityAction action)
    {
        if (actionsOnPaused != null)
        {
            actionsOnPaused.Add(action);
        }
    }

    public void AddListenerOnPauseStop(UnityAction action)
    {
        if(actionsOnPauseStop!=null)
        {
            actionsOnPauseStop.Add(action);
        }
    }

    public void InvokePause()
    {
        if (actionsOnPaused != null)
        {
            for (int i = 0; i < actionsOnPaused.Count; i++)
            {
                actionsOnPaused[i].Invoke();
            }
        }
    }

    public void InvokeOnPauseStop()
    {
        if(actionsOnPauseStop != null)
        {
            for(int i = 0;i < actionsOnPauseStop.Count;i++)
            {
                actionsOnPauseStop[i].Invoke();
            }
        }
    }
}
