using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    readonly (float scale,float pos) emptyXScalePos = (0,-0.4f); // bad magic
    readonly (float scale,float pos) fullXScalePos = (0.8f,0); // bad magic

    private void OnEnable() 
    {
        Refresh(0);
    }

    public void Refresh(float pct)
    {
        pct = Mathf.Min(1,pct);
        // a + (b - a) * t.
        Vector3 newScale = bar.transform.localScale;
        Vector3 newPos = bar.transform.localPosition;

        newScale.x = emptyXScalePos.scale + (fullXScalePos.scale - emptyXScalePos.scale) * pct;
        newPos.x = emptyXScalePos.pos + (fullXScalePos.pos - emptyXScalePos.pos) * pct;

        bar.localScale = newScale;
        bar.localPosition = newPos;
    }

}
