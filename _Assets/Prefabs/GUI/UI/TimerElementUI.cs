using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerElementUI : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float fillPct = 1f;
    [SerializeField] Image fillCircle;

    public float FillPct
    {
        get{return fillPct;}
        set{fillPct = Mathf.Clamp(value,0,1);}
    }

    void FixedUpdate() 
    {
        fillCircle.fillAmount = fillPct;    
    }

}
