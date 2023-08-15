using UnityEngine;

public class ShowInteractionButton : MonoBehaviour
{
    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";

    private bool _isShowing = false;

    [SerializeField] private Animator _interactionPanelAnimator;

    public void ShowOrHide()
    {
        if (!_isShowing)
        {
            _interactionPanelAnimator.SetTrigger(SHOW_TRIGGER);
            _isShowing = true;
        }
        else if (_interactionPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _isShowing = false;
        }
        else
        {
            _interactionPanelAnimator.SetTrigger(HIDE_TRIGGER);
            _isShowing = false;
        }
    }
}
