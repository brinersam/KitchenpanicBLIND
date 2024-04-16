using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class UI_Element_Countdown : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void StartSequence(int seconds, Action callback)
    {   
        seconds = Mathf.Max(seconds,0);

        gameObject.SetActive(true);

        _text.text = seconds.ToString();
                                            
        var sequence = DOTween.Sequence().SetUpdate(true);

        Vector3 strengthBounce = Vector3.one * 1.1f;
        float secPerBounce = 1;
        for(int cntdwn = seconds-1; cntdwn >= 0; cntdwn--)
        {
            string displayNumber = cntdwn.ToString();
            sequence.Append(transform
                .DOPunchScale(strengthBounce, secPerBounce, vibrato: 2)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => _text.text = displayNumber)
            );
        }
        sequence.OnComplete(() => WhenDone(callback));
    }
    private void WhenDone(Action callback)
    {
        Debug.Log("done");
        callback();
        gameObject.SetActive(false);
    }

}
