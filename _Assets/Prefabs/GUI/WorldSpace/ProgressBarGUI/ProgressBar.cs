using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Transform bar;


    private bool panicOdd;
    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip ProgressBar_Warning;
    [SerializeField] AudioClip ProgressBar_WarningFinal;

    [SerializeField] GameObject warningBlinkerObj;

    private Color defaultClr;
    private SpriteRenderer fillBarRndrr;

    readonly (float scale,float pos) emptyXScalePos = (0,-0.4f); // bad magic
    readonly (float scale,float pos) fullXScalePos = (0.8f,0); // bad magic

    void Awake()
    {
        fillBarRndrr = bar.GetComponent<SpriteRenderer>();
        defaultClr = fillBarRndrr.color;
    }

    void OnEnable() 
    {
        Refresh(0);
        fillBarRndrr.color = defaultClr;
        warningBlinkerObj.SetActive(false);
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

    public void Warning_Pulse(bool extraBad = false)
    {
        if (!warningBlinkerObj.activeSelf)
            warningBlinkerObj.SetActive(true);

        panicOdd = !panicOdd;

        if (panicOdd) // first
        {
            fillBarRndrr.color = Color.red;
            warningBlinkerObj.transform.localScale *= 2f;
            
        }
        else if (panicOdd == false) // second
        {
            fillBarRndrr.color = Color.white;
            warningBlinkerObj.transform.localScale = Vector3.one;
        }

        audioSrc.PlayOneShot(ProgressBar_Warning,0.7f);

        if (extraBad)
            audioSrc.PlayOneShot(ProgressBar_WarningFinal);
    }

}
