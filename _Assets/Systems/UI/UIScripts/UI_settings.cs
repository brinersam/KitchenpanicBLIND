using UnityEngine;
using UnityEngine.UI;

public class UI_settings : Abstract_UI
{
    [SerializeField] private Button _backButton;
    private Abstract_UI _prevState;

    public override void Activate(Abstract_UI prevstate = null)
    {
        _prevState = prevstate;
        gameObject.SetActive(true);
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(OnButton);
    }

    private void OnButton()
    {
        System_UI.state.SetStateTo(_prevState);
    }
}
