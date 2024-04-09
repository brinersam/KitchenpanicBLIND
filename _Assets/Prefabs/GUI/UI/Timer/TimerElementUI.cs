using UnityEngine;
using UnityEngine.UI;

public class TimerElementUI : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float fillPct = 1f;
    [SerializeField] Image fillCircle;

    private void Awake()
    {
        System_Tick.OnTick += OnTick;
    }

    public void SetVisual(float pct)
    {
        fillPct = Mathf.Clamp(pct,0,1);
    }

    private void OnTick()
    {
        fillCircle.fillAmount = fillPct;
    }


}
