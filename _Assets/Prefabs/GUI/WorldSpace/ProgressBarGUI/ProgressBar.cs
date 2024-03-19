using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    [SerializeField] AudioSource panicSound;
    [SerializeField] AudioClip ProgressBar_Warning;
    [SerializeField] AudioClip ProgressBar_WarningFinal;
    [SerializeField] GameObject warningBlinkerUI;

    readonly (float scale,float pos) emptyXScalePos = (0,-0.4f); // bad magic
    readonly (float scale,float pos) fullXScalePos = (0.8f,0); // bad magic

    void OnEnable() 
    {
        Refresh(0);
        warningBlinkerUI.SetActive(false);
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

    public void Warning_Pulse(bool final = false)
    {
        if (!warningBlinkerUI.activeSelf)
            warningBlinkerUI.SetActive(true);

        if (final)
            panicSound.PlayOneShot(ProgressBar_WarningFinal);
        else
            panicSound.PlayOneShot(ProgressBar_Warning,0.7f);

        warningBlinkerUI.transform.Rotate(Vector3.forward * 34);
    }

}
