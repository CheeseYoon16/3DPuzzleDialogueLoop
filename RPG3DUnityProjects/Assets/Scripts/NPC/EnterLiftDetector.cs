using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterLiftDetector : MonoBehaviour
{
    private const string NPCTAG = "NPC"; //means the value is constant, cant be changed

    public UnityEvent OnNPCEnterLift = new UnityEvent();


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == NPCTAG)
        {
            Debug.Log("NPC has enter the lift");
            OnNPCEnterLift?.Invoke();

            if(Timer.Instance != null)
            {
                Timer.Instance.TimerLoseDetection();
            }
        }
    }

}
