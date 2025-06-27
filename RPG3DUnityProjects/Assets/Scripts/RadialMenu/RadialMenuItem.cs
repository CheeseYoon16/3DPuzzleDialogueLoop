using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuItem : MonoBehaviour
{
    [SerializeField] Image circularBackground;
    [SerializeField] Image iconHolder;

    Image CircularBG => circularBackground;

    public void Init(float size)
    {
        SetCircularBG(size);
    }

    private void SetCircularBG(float size)
    {
        if(CircularBG != null)
        {
            Debug.Log($"size delta is: {size}");
            CircularBG.fillAmount = size;
        }
    }
}
