using DG.Tweening;
using UnityEngine;

public class WaitingScreenController : MonoBehaviour, IShow
{
    public void ShowScreen(System.Action callback = null)
    {
        GetComponent<CanvasGroup>()
        .DOFade(1, UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "show_waiting_screen"))
        .OnComplete(() => callback?.Invoke());
    }

    public void HideScreen(System.Action callback = null)
    {
        GetComponent<CanvasGroup>()
        .DOFade(0, UniversalConfigParser.GetFloatParam(UniversalConfigParser.GetNodesByTag("appParams"), "hide_waiting_screen"))
        .OnComplete(() => callback?.Invoke()); ;
    }
}
